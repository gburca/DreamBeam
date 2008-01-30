
;--------------------------------
; Include Modern UI
	
	!include "MUI.nsh"
	!include "WinMessages.nsh"
	!include "versions.nsh"
	!include "GetVersions.nsh"
	!include "CheckDependencies.nsh"

;--------------------------------
; General

        ; Name and file
        !define VERSION "0.72"
        !define PRODUCT "DreamBeam"
	Name "${PRODUCT} ${VERSION}"
	
	!ifdef FullInstall
		OutFile "Installer\${PRODUCT}_Full_${VERSION}.exe"
	!else
		OutFile "Installer\${PRODUCT}_${VERSION}.exe"
	!endif

	
	; Default installation folder
	InstallDir "$PROGRAMFILES\DreamBeam"

	; Get installation folder from registry if available
	InstallDirRegKey HKLM "Software\${PRODUCT}" ""

	; In MS-Vista, all user-modifiable files should be stored outside the
	; ProgramFiles directory or the user won't be able to modify them.
	Var /GLOBAL USERFILES

	SetCompressor lzma


;--------------------------------
; Interface Configuration

	!define MUI_HEADERIMAGE
	!define MUI_HEADERIMAGE_BITMAP "images\banner2.bmp"
	!define MUI_WELCOMEFINISHPAGE_BITMAP "images\balken.bmp"
	!define MUI_ABORTWARNING
	!define MUI_FINISHPAGE_RUN DreamBeam.exe
	!define MUI_FINISHPAGE_RUN_NOTCHECKED

;--------------------------------
; Pages
	!insertmacro MUI_PAGE_WELCOME
	!insertmacro MUI_PAGE_LICENSE "Images\License.rtf"
	!insertmacro MUI_PAGE_COMPONENTS
	!insertmacro MUI_PAGE_DIRECTORY
	; Sample files
	!define MUI_DIRECTORYPAGE_TEXT_TOP "Program settings and data directory. This is where the sample files will be installed and where DreamBeam will expect to find songs and other files. All files created by DreamBeam will also be saved to this directory by default."
	!define MUI_DIRECTORYPAGE_TEXT_DESTINATION "Program settings and data directory"
	!define MUI_DIRECTORYPAGE_VARIABLE $USERFILES
	!define MUI_DIRECTORYPAGE_VERIFYONLEAVE
	!insertmacro MUI_PAGE_DIRECTORY

	!insertmacro MUI_PAGE_INSTFILES
	!insertmacro MUI_PAGE_FINISH

	!insertmacro MUI_UNPAGE_CONFIRM
	!insertmacro MUI_UNPAGE_COMPONENTS
	!insertmacro MUI_UNPAGE_INSTFILES
  
;--------------------------------------
; !insertmacro MUI_WELCOMEPAGE_TITLE "DreamBeam Installation"

;--------------------------------
; Languages
	!insertmacro MUI_LANGUAGE "English"


!insertmacro Checker

Function .onInit
	SetShellVarContext all
	; Set the default location for user created files
	; C:\Documents and Settings\All Users\Application Data\DreamBeam
	StrCpy $USERFILES "$APPDATA\${PRODUCT}"
	; C:\Documents and Settings\All Users\DreamBeam
	;StrCpy $USERFILES "$DOCUMENTS\${PRODUCT}"
FunctionEnd


Section "DreamBeam" SDreamBeam

	SetOutPath "$INSTDIR"

	; DreamBeam...
	File /r /x .svn /x *.xml DreamBeam\*.*
	File /r /x .svn Libs\*.*
	
	RegDll "$INSTDIR\ActiveDiatheke.ocx"
	
	; Create desktop and start menu entries
	CreateDirectory "$SMPROGRAMS\${PRODUCT}"
	CreateShortCut "$SMPROGRAMS\DreamBeam\DreamBeam.lnk" "$INSTDIR\DreamBeam.exe"
	CreateShortCut "$SMPROGRAMS\DreamBeam\Uninstall.lnk" "$INSTDIR\Uninstall.exe"
	CreateShortCut "$DESKTOP\DreamBeam.lnk" "$INSTDIR\DreamBeam.exe"

	
	; Store installation folder
	WriteRegStr HKLM "Software\${PRODUCT}" "" $INSTDIR
  
	; Save the location of the User-file directory so the app knows where to find them
	WriteRegStr HKEY_LOCAL_MACHINE "Software\${PRODUCT}" "UserFilesDir" "$USERFILES"

	; Write uninstall strings
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT}" "DisplayName" "${PRODUCT} (remove only)"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT}" "UninstallString" '"$INSTDIR\Uninstall.exe"'
	
	; Create uninstaller
	WriteUninstaller "$INSTDIR\Uninstall.exe"

	; A place where we can create logs
	CreateDirectory "$USERFILES"
SectionEnd

; Subsections should default to optional if this is an update so that
; we don't overwrite the user's data with the sample files.
SectionGroup /e "Sample Files" SSampleFiles
	Section /o "Configuration files" SConfigFiles
	        SetOutPath "$USERFILES"
	        File /r /x .svn SampleFiles\ConfigFiles\*.*
	SectionEnd
	
	Section /o "Songs" SSongs
		SetOutPath "$USERFILES\Songs"
		File /r /x .svn SampleFiles\Songs\*.*
	SectionEnd
	
	Section /o "Backgrounds" SBackgrounds
	        SetOutPath "$USERFILES\Backgrounds"
	        File /r /x .svn SampleFiles\Backgrounds\*.*
	SectionEnd
	
	Section /o "Media files" SMediaFiles
	        SetOutPath "$USERFILES\MediaFiles"
		File /r /x .svn SampleFiles\MediaFiles\*.*
	SectionEnd

	Section /o "Media lists" SMediaLists
		SetOutPath "$USERFILES\MediaLists"
		File /r /x .svn SampleFiles\MediaLists\*.*
	SectionEnd
SectionGroupEnd

;--------------------------------
; Create Read_USERFILES and un.Read_USERFILES functions
!macro Read_USERFILES UN
Function ${UN}Read_USERFILES
	; Find current user-files directory
	ReadRegStr $USERFILES HKLM "Software\${PRODUCT}" "UserFilesDir"
FunctionEnd
!macroend

!insertmacro Read_USERFILES ""
!insertmacro Read_USERFILES "un."


;--------------------------------
; Uninstaller Section
UninstallText "This will uninstall ${PRODUCT} and delete all files created by ${PRODUCT}. Please backup all files you wish to keep. Hit Uninstall to continue."

Section "un.Uninstall DreamBeam" SUnDreamBeam
	SetShellVarContext all
	Call un.Read_USERFILES
	
	UnRegDll "$INSTDIR\ActiveDiatheke.ocx"

	; Delete DreamBeam Files
	Delete "$INSTDIR\*.*"
	Delete "$SMPROGRAMS\DreamBeam\DreamBeam.lnk"
	Delete "$DESKTOP\DreamBeam.lnk"
	RMDir /r "$INSTDIR\de-DE"
	RMDir /r "$INSTDIR\de"
	RMDir /r "$INSTDIR\fr"
	RMDir /r "$INSTDIR\Sword"

	DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT}"

	; Delete Startmenu Entries
	RMDir	/r "$SMPROGRAMS\${PRODUCT}"
	DeleteRegValue HKLM "Software\${PRODUCT}" "UserFilesDir"	; Remove $USERFILES entry
	DeleteRegKey /ifempty HKLM "Software\${PRODUCT}"

	; Delete Uninstaller
	Delete "$INSTDIR\Uninstall.exe"
	
	; Remove install directory (only if empty)
	RMDir "$INSTDIR"

	; This should contain the directory returned by Tools.GetAppCachePath() in the App.
	RMDir /r "$APPDATA\${PRODUCT}\Cache"
	Delete "$APPDATA\${PRODUCT}\LogFile.txt"
	RMDir "$APPDATA\${PRODUCT}" ; Remove user directory (only if empty)

SectionEnd


SectionGroup /e "un.Uninstall Application Files" SUnAppFiles
	Section /o "un.Configuration files" SUnConfigFiles
		Call un.Read_USERFILES
		Delete "$USERFILES\*.xml"
		RMDir "$USERFILES" ; Remove user directory (only if empty)
	SectionEnd

	Section /o "un.Song files" SUnSongs
		Call un.Read_USERFILES
		RMDir /r "$USERFILES\Songs"
		RMDir "$USERFILES" ; Remove user directory (only if empty)
	SectionEnd

	Section /o "un.Backgrounds" SUnBackgrounds
		Call un.Read_USERFILES
		RMDir /r "$USERFILES\Backgrounds"
		RMDir "$USERFILES" ; Remove user directory (only if empty)
	SectionEnd

	Section /o "un.Media files" SUnMediaFiles
		Call un.Read_USERFILES
		RMDir /r "$USERFILES\MediaFiles"
		RMDir "$USERFILES" ; Remove user directory (only if empty)
	SectionEnd

	Section /o "un.Media lists" SUnMediaLists
		Call un.Read_USERFILES
		RMDir /r "$USERFILES\MediaLists"
		RMDir "$USERFILES" ; Remove user directory (only if empty)
	SectionEnd

SectionGroupEnd


;--------------------------------
; Section descriptions
LangString DESC_SDreamBeam	${LANG_ENGLISH} "Main application"
LangString DESC_SSampleFiles	${LANG_ENGLISH} "Sample files"
LangString DESC_SConfigFiles	${LANG_ENGLISH} "Configuration files"
LangString DESC_SSongs		${LANG_ENGLISH} "Song files"
LangString DESC_SBackgrounds	${LANG_ENGLISH} "Background graphic files"
LangString DESC_SMediaFiles	${LANG_ENGLISH} "Multimedia files"
LangString DESC_SMediaLists	${LANG_ENGLISH} "Multimedia lists"
; The Uninstall sections
LangString DESC_SUnDreamBeam	${LANG_ENGLISH} "Main application"
LangString DESC_SUnAppFiles	${LANG_ENGLISH} "Uninstall/delete various files created by DreamBeam"
LangString DESC_SUnConfigFiles	${LANG_ENGLISH} "Uninstall/delete configuration files"
LangString DESC_SUnSongs	${LANG_ENGLISH} "Uninstall/delete sample and user-created Song files"
LangString DESC_SUnBackgrounds	${LANG_ENGLISH} "Uninstall/delete background graphic files"
LangString DESC_SUnMediaFiles	${LANG_ENGLISH} "Uninstall/delete multimedia files"
LangString DESC_SUnMediaLists	${LANG_ENGLISH} "Uninstall/delete multimedia lists"


!insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
  ; The Install sections
  !insertmacro MUI_DESCRIPTION_TEXT ${SDreamBeam}	$(DESC_SDreamBeam)
  !insertmacro MUI_DESCRIPTION_TEXT ${SSampleFiles}	$(DESC_SSampleFiles)
  !insertmacro MUI_DESCRIPTION_TEXT ${SConfigFiles}	$(DESC_SConfigFiles)
  !insertmacro MUI_DESCRIPTION_TEXT ${SSongs}		$(DESC_SSongs)
  !insertmacro MUI_DESCRIPTION_TEXT ${SBackgrounds}	$(DESC_SBackgrounds)
  !insertmacro MUI_DESCRIPTION_TEXT ${SMediaFiles}	$(DESC_SMediaFiles)
  !insertmacro MUI_DESCRIPTION_TEXT ${SMediaLists}	$(DESC_SMediaLists)
!insertmacro MUI_FUNCTION_DESCRIPTION_END

!insertmacro MUI_UNFUNCTION_DESCRIPTION_BEGIN
  ; The Uninstall sections
  !insertmacro MUI_DESCRIPTION_TEXT ${SUnDreamBeam}	$(DESC_SUnDreamBeam)
  !insertmacro MUI_DESCRIPTION_TEXT ${SUnAppFiles}	$(DESC_SUnAppFiles)
  !insertmacro MUI_DESCRIPTION_TEXT ${SUnConfigFiles}	$(DESC_SUnConfigFiles)
  !insertmacro MUI_DESCRIPTION_TEXT ${SUnSongs}		$(DESC_SUnSongs)
  !insertmacro MUI_DESCRIPTION_TEXT ${SUnBackgrounds}	$(DESC_SUnBackgrounds)
  !insertmacro MUI_DESCRIPTION_TEXT ${SUnMediaFiles}	$(DESC_SUnMediaFiles)
  !insertmacro MUI_DESCRIPTION_TEXT ${SUnMediaLists}	$(DESC_SUnMediaLists)
!insertmacro MUI_UNFUNCTION_DESCRIPTION_END




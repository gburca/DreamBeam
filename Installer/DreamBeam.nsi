
;--------------------------------
; Include Modern UI
	
	!include "MUI.nsh"
	!include "WinMessages.nsh"
	!include "versions.nsh"
	!include "GetVersions.nsh"
	!include "CheckDependencies.nsh"
	!include "UAC.nsh"

;--------------------------------
; General

        ; Name and file.
        !define VERSION "0.9a"

	; This ${PRODUCT} !define is used throughout this intaller for a lot of
	; things including install directory names and links. It should probably
	; never be changed.
        !define PRODUCT "DreamBeam"

	Name "${PRODUCT} ${VERSION}"
	
	!ifdef FullInstall
		OutFile "InstallerOutput\${PRODUCT}_Full_${VERSION}.exe"
	!else
		OutFile "InstallerOutput\${PRODUCT}_${VERSION}.exe"
	!endif
	
	; Default installation folder
	InstallDir "$PROGRAMFILES\${PRODUCT}"

	; Get installation folder from registry if available
	InstallDirRegKey HKLM "Software\${PRODUCT}" ""

	; In MS-Vista, all user-modifiable files should be stored outside the
	; ProgramFiles directory or the user won't be able to modify them.
	Var /GLOBAL USERFILES

	; Used for the uninstaller
	Var /GLOBAL ShouldCleanRegistry

	SetCompressor lzma
	ShowInstDetails show

	; Request application privileges for Windows Vista
	RequestExecutionLevel user

;--------------------------------
; Interface Configuration

	!define MUI_HEADERIMAGE
	!define MUI_HEADERIMAGE_BITMAP "images\banner2.bmp"
	!define MUI_WELCOMEFINISHPAGE_BITMAP "images\balken.bmp"
	!define MUI_ABORTWARNING
	; Doesn't seem to work. How do we specify the full path?
	;!define MUI_FINISHPAGE_RUN "DreamBeam.exe"
	;!define MUI_FINISHPAGE_RUN_NOTCHECKED

;--------------------------------
; Pages
	!insertmacro MUI_PAGE_WELCOME
	!insertmacro MUI_PAGE_LICENSE "Images\License.rtf"
	!insertmacro MUI_PAGE_COMPONENTS
	!insertmacro MUI_PAGE_DIRECTORY
	; Sample files
	!define MUI_DIRECTORYPAGE_TEXT_TOP "Program settings and data directory. This is where the sample files will be installed and where ${PRODUCT} will expect to find songs and other files. All files created by ${PRODUCT} will also be saved to this directory by default."
	!define MUI_DIRECTORYPAGE_TEXT_DESTINATION "Program settings and data directory"
	!define MUI_DIRECTORYPAGE_VARIABLE $USERFILES
	!define MUI_DIRECTORYPAGE_VERIFYONLEAVE
	!insertmacro MUI_PAGE_DIRECTORY

	!insertmacro MUI_PAGE_INSTFILES

	!define MUI_FINISHPAGE_TEXT "${PRODUCT} ${VERSION} has been installed on your computer.\r\n\r\nIf upgrading from a version of ${PRODUCT} prior to 0.80, you should copy all your songs, backgrounds, etc... to the program settings and data directory you selected earlier in the installation process. The location of this directory is also shown in the Options dialog box once ${PRODUCT} is running.\r\n\r\nClick Finish to close this wizard."
	!insertmacro MUI_PAGE_FINISH

	!insertmacro MUI_UNPAGE_CONFIRM
	!insertmacro MUI_UNPAGE_COMPONENTS
	!insertmacro MUI_UNPAGE_INSTFILES
  
;--------------------------------------
; !insertmacro MUI_WELCOMEPAGE_TITLE "${PRODUCT} Installation"

;--------------------------------
; Languages
	!insertmacro MUI_LANGUAGE "English"


!insertmacro Checker

Function .onInit
	SetShellVarContext all
	; Set the default location for user created files

	; In Vista Home, this directory is hidden, so it's not a good place to use
	; C:\Documents and Settings\All Users\Application Data\DreamBeam
	; StrCpy $USERFILES "$APPDATA\${PRODUCT}"

	; C:\Documents and Settings\All Users\DreamBeam
	StrCpy $USERFILES "$DOCUMENTS\${PRODUCT}"

	; Attempt to give the UAC plug-in a user process and an admin process.
	UAC_Elevate:
	    UAC::RunElevated 
	    StrCmp 1223 $0 UAC_ElevationAborted ; UAC dialog aborted by user?
	    StrCmp 0 $0 0 UAC_Err ; Error?
	    StrCmp 1 $1 0 UAC_Success ;Did everything worked correctly ?
	    Quit
	    
	UAC_Err:
	    MessageBox mb_iconstop "Unable to elevate, error $0"
	    Abort
	
	UAC_ElevationAborted:
	    # elevation was aborted, run as normal?
	    MessageBox mb_iconstop "This installer requires admin access, aborting!"
	    Abort
	
	UAC_Success:
	    StrCmp 1 $3 +4 ;Admin?
	    StrCmp 3 $1 0 UAC_ElevationAborted ;Try again?
	    MessageBox mb_iconstop "This installer requires admin access, try again"
	    goto UAC_Elevate 
    
FunctionEnd

Function .OnInstFailed
    UAC::Unload ;Must call unload!
FunctionEnd
Function .OnInstSuccess
    UAC::Unload ;Must call unload!
FunctionEnd

;Function un.OnUnInstFailed
;    UAC::Unload ;Must call unload!
;FunctionEnd
;Function un.OnUnInstSuccess
;    UAC::Unload ;Must call unload!
;FunctionEnd


Section "DreamBeam" SDreamBeam

	SetOutPath "$INSTDIR"

	; DreamBeam...
	File /r /x .svn /x *.xml DreamBeam\*.*
	File /r /x .svn Libs\*.*

	SetOutPath "$INSTDIR\Help"
	File /r /x .svn Help\*.*
	
	; Create desktop and start menu entries
	; Apparently the OutPath determines the "Start in" directory used for the shortcut.
	SetOutPath "$INSTDIR"
	CreateDirectory "$SMPROGRAMS\${PRODUCT}"
	CreateShortCut "$SMPROGRAMS\${PRODUCT}\${PRODUCT}.lnk" "$INSTDIR\DreamBeam.exe"
	CreateShortCut "$SMPROGRAMS\${PRODUCT}\Uninstall.lnk" "$INSTDIR\Uninstall.exe"
	CreateShortCut "$DESKTOP\${PRODUCT}.lnk" "$INSTDIR\DreamBeam.exe"

	
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
	Section "Configuration files" SConfigFiles
	        SetOutPath "$USERFILES"
	        File /r /x .svn SampleFiles\ConfigFiles\*.*
	SectionEnd
	
	Section "Songs" SSongs
		SetOutPath "$USERFILES\Songs"
		File /r /x .svn SampleFiles\Songs\*.*
	SectionEnd
	
	Section "Backgrounds" SBackgrounds
	        SetOutPath "$USERFILES\Backgrounds"
	        File /r /x .svn SampleFiles\Backgrounds\*.*
	SectionEnd
	
	Section "Media files" SMediaFiles
	        SetOutPath "$USERFILES\MediaFiles"
		File /r /x .svn SampleFiles\MediaFiles\*.*
	SectionEnd

	Section "Media lists" SMediaLists
		SetOutPath "$USERFILES\MediaLists"
		File /r /x .svn SampleFiles\MediaLists\*.*
	SectionEnd

	Section "Themes" SThemes
		SetOutPath "$USERFILES\Themes"
		File /nonfatal /r /x .svn SampleFiles\Themes\*.*
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
	;Var /GLOBAL ShouldCleanRegistry
	SetShellVarContext all
	Call un.Read_USERFILES
	
	; Delete DreamBeam Files
	Delete "$SMPROGRAMS\${PRODUCT}\${PRODUCT}.lnk"
	Delete "$DESKTOP\${PRODUCT}.lnk"

	DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT}"
	StrCpy $ShouldCleanRegistry "YES"

	; Delete Startmenu Entries
	RMDir /r "$SMPROGRAMS\${PRODUCT}"

	; Delete Uninstaller
	Delete "$INSTDIR\Uninstall.exe"
	
	; Remove install directory
	RMDir /r /REBOOTOK "$INSTDIR"
	; Or delete things one by one. But it's very easy to forget some things this way.
	;Delete "$INSTDIR\*.*"
	;RMDir /r "$INSTDIR\de-DE"
	;RMDir /r "$INSTDIR\de"
	;RMDir /r "$INSTDIR\fr"
	;RMDir /r "$INSTDIR\Sword"


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

	Section /o "un.Themes" SUnThemes
		Call un.Read_USERFILES
		RMDir /r "$USERFILES\Themes"
		RMDir "$USERFILES" ; Remove user directory (only if empty)
	SectionEnd

SectionGroupEnd

Section "-un.Registry entries"
	; This section must be the last uninstall section

	; We must wait until the end to clean the registry because SUnAppFiles
	; needs it to figure out where all the user files are.

	; We only clean the regitry if the main application is being uninstalled
	StrCmp $ShouldCleanRegistry "YES" CleanRegistry CleanRegistryDone

	CleanRegistry:
	DeleteRegValue HKLM "Software\${PRODUCT}" "UserFilesDir"	; Remove $USERFILES entry
	DeleteRegValue HKLM "Software\${PRODUCT}" ""			; Remove default entry
	DeleteRegKey /ifempty HKLM "Software\${PRODUCT}"

	CleanRegistryDone:

SectionEnd

;--------------------------------
; Section descriptions
LangString DESC_SDreamBeam	${LANG_ENGLISH} "Main application"
LangString DESC_SSampleFiles	${LANG_ENGLISH} "Sample files. You should install all the sample files unless you already have a version >= 0.80 of ${PRODUCT} installed."
LangString DESC_SConfigFiles	${LANG_ENGLISH} "Configuration files"
LangString DESC_SSongs		${LANG_ENGLISH} "Song files"
LangString DESC_SBackgrounds	${LANG_ENGLISH} "Background graphic files"
LangString DESC_SMediaFiles	${LANG_ENGLISH} "Multimedia files"
LangString DESC_SMediaLists	${LANG_ENGLISH} "Multimedia lists"
LangString DESC_SThemes		${LANG_ENGLISH} "Theme files"
; The Uninstall sections
LangString DESC_SUnDreamBeam	${LANG_ENGLISH} "Main application"
LangString DESC_SUnAppFiles	${LANG_ENGLISH} "Uninstall/delete various files created by ${PRODUCT}"
LangString DESC_SUnConfigFiles	${LANG_ENGLISH} "Uninstall/delete configuration files"
LangString DESC_SUnSongs	${LANG_ENGLISH} "Uninstall/delete sample and user-created Song files"
LangString DESC_SUnBackgrounds	${LANG_ENGLISH} "Uninstall/delete background graphic files"
LangString DESC_SUnMediaFiles	${LANG_ENGLISH} "Uninstall/delete multimedia files"
LangString DESC_SUnMediaLists	${LANG_ENGLISH} "Uninstall/delete multimedia lists"
LangString DESC_SUnThemes	${LANG_ENGLISH} "Uninstall/delete theme files"


!insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
  ; The Install sections
  !insertmacro MUI_DESCRIPTION_TEXT ${SDreamBeam}	$(DESC_SDreamBeam)
  !insertmacro MUI_DESCRIPTION_TEXT ${SSampleFiles}	$(DESC_SSampleFiles)
  !insertmacro MUI_DESCRIPTION_TEXT ${SConfigFiles}	$(DESC_SConfigFiles)
  !insertmacro MUI_DESCRIPTION_TEXT ${SSongs}		$(DESC_SSongs)
  !insertmacro MUI_DESCRIPTION_TEXT ${SBackgrounds}	$(DESC_SBackgrounds)
  !insertmacro MUI_DESCRIPTION_TEXT ${SMediaFiles}	$(DESC_SMediaFiles)
  !insertmacro MUI_DESCRIPTION_TEXT ${SMediaLists}	$(DESC_SMediaLists)
  !insertmacro MUI_DESCRIPTION_TEXT ${SThemes}		$(DESC_SThemes)
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
  !insertmacro MUI_DESCRIPTION_TEXT ${SUnThemes}	$(DESC_SUnThemes)
!insertmacro MUI_UNFUNCTION_DESCRIPTION_END




;--------------------------------
;Include Modern UI
	
	!include "MUI.nsh"
	!include "WinMessages.nsh"
	!include "versions.nsh"
	!include "GetVersions.nsh"
	!include "CheckDependencies.nsh"

;--------------------------------
;General

        ;Name and file
        !define VERSION "0.7"
        !define PRODUCT "Dreambeam"
	Name "${PRODUCT} ${VERSION}"
	
	!ifdef FullInstall
		OutFile "Installer\${PRODUCT}_Full_${VERSION}.exe"
	!else
		OutFile "Installer\${PRODUCT}_${VERSION}.exe"
	!endif

	
	;Default installation folder
	InstallDir "$PROGRAMFILES\DreamBeam"

	;Get installation folder from registry if available
	InstallDirRegKey HKLM "Software\${PRODUCT}" ""

	SetCompressor lzma  
	

;--------------------------------
;Interface Configuration

	!define MUI_HEADERIMAGE
	!define MUI_HEADERIMAGE_BITMAP "images\banner2.bmp"
	!define MUI_WELCOMEFINISHPAGE_BITMAP "images\balken.bmp"
	!define MUI_ABORTWARNING
	!define MUI_FINISHPAGE_RUN Dreambeam.exe

;--------------------------------
;Pages
	!insertmacro MUI_PAGE_WELCOME
	!insertmacro MUI_PAGE_LICENSE "Images\License.rtf"
	!insertmacro MUI_PAGE_COMPONENTS
	!insertmacro MUI_PAGE_DIRECTORY
	!insertmacro MUI_PAGE_INSTFILES
	!insertmacro MUI_PAGE_FINISH

	!insertmacro MUI_UNPAGE_CONFIRM
	!insertmacro MUI_UNPAGE_INSTFILES
  
;--------------------------------------
;!insertmacro MUI_WELCOMEPAGE_TITLE "Dreambeam Installation"

;--------------------------------
;Languages
	!insertmacro MUI_LANGUAGE "English"



!insertmacro Checker


Section "DreamBeam"

	SetOutPath "$INSTDIR"

	;Dreambeam...
	File /r /x .svn /x *.xml Dreambeam\*.*
	File /r /x .svn Libs\*.*
	
	RegDll "$INSTDIR\ActiveDiatheke.ocx"
	
	;Create desktop and start menu entries
	CreateDirectory "$SMPROGRAMS\${PRODUCT}"
	CreateShortCut "$SMPROGRAMS\Dreambeam\Dreambeam.lnk" "$INSTDIR\Dreambeam.exe"
	CreateShortCut "$SMPROGRAMS\Dreambeam\Uninstall.lnk" "$INSTDIR\uninstall.exe"
	CreateShortCut "$DESKTOP\Dreambeam.lnk" "$INSTDIR\Dreambeam.exe"

	
	;Store installation folder
	WriteRegStr HKLM "Software\${PRODUCT}" "" $INSTDIR
  
	;Write uninstall strings
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Dreambeam" "DisplayName" "Dreambeam (remove only)"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Dreambeam" "UninstallString" '"$INSTDIR\Uninstall.exe"'
	
	;Create uninstaller
	WriteUninstaller "$INSTDIR\Uninstall.exe"

SectionEnd

; Subsections should default to optional if this is an update so that
; we don't overwrite the user's data with the sample files.
SectionGroup /e "Sample Files"
	Section /o "Configuration files"
	        SetOutPath "$INSTDIR"
	        File /r /x .svn SampleFiles\ConfigFiles\*.*
	SectionEnd
	
	Section /o "Songs"
		SetOutPath "$INSTDIR\Songs"
		File /r /x .svn SampleFiles\Songs\*.*
	SectionEnd
	
	Section /o "Backgrounds"
	        SetOutPath "$INSTDIR\Backgrounds"
	        File /r /x .svn SampleFiles\Backgrounds\*.*
	SectionEnd
	
	Section /o "Media files"
	        SetOutPath "$INSTDIR\MediaFiles"
		File /r /x .svn SampleFiles\MediaFiles\*.*
		SetOutPath "$INSTDIR\MediaLists"
		File /r /x .svn SampleFiles\MediaLists\*.*
	SectionEnd
SectionGroupEnd

;--------------------------------
;Uninstaller Section
UninstallText "This will uninstall ${PRODUCT} and delete all files in the $INSTDIR directory. Move files you wish to keep out of that directory. Hit Uninstall to continue."
Section "un.Uninstall"

	;Delete Dreambeam Files
	Delete "$INSTDIR\*.*"
	Delete "$SMPROGRAMS\Dreambeam\Dreambeam.lnk"
	Delete "$DESKTOP\Dreambeam.lnk"
	RMDir /r "$INSTDIR\de-DE"
	RMDir /r "$INSTDIR\de"
	RMDir /r "$INSTDIR\fr"
	RMDir /r "$INSTDIR\Sword"

	DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Dreambeam"

	MessageBox MB_YESNO|MB_ICONQUESTION "Would you like to remove all your Songs, MediaFiles and MediaLists?" IDNO NoSongDelete
	RMDir /r "$INSTDIR\Songs" ; skipped if no
	RMDir /r "$INSTDIR\MediaFiles" ; skipped if no
	RMDir /r "$INSTDIR\MediaLists" ; skipped if no

  NoSongDelete:	

	MessageBox MB_YESNO|MB_ICONQUESTION "Would you like to remove all your Backgrounds?" IDNO NoBGDelete   
	RMDir /r "$INSTDIR\Backgrounds" ; skipped if no


  NoBGDelete:		

	;Delete Startmenu Entries
	RMDir	/r "$SMPROGRAMS\${PRODUCT}"
	DeleteRegKey /ifempty HKLM "Software\${PRODUCT}"

	;Delete Uninstaller
	Delete "$INSTDIR\Uninstall.exe"
	
	; Remove install directory (only if empty)
	RMDir "$INSTDIR"

SectionEnd

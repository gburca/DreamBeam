; Define Fullinstall, to include .Net und DirectX
;!define FullInstall

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
  !define VERSION "0.5"
	!define MUI_PRODUCT "Dreambeam"
	Name "${MUI_PRODUCT} ${VERSION}"
	
	!ifdef FullInstall
		OutFile "Installer\${MUI_PRODUCT}_Full_${VERSION}.exe"
	!else
		OutFile "Installer\${MUI_PRODUCT}_${VERSION}.exe"
	!endif

	
  ;Default installation folder
  InstallDir "$PROGRAMFILES\Dreambeam"
  
  ;Get installation folder from registry if available
  InstallDirRegKey HKLM "Software\${MUI_PRODUCT}" ""

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


;!define MUI_TEXT_WELCOME_INFO_TEXT "\nIt is recommended that you close all other applications before resuming this program.\n\nClick Next to continue.$\n"



!insertmacro Checker



Section "Dreambeam" 

  SetOutPath "$INSTDIR"

  ;Dreambeam...
	File /r Dreambeam\*.*
	File /r Libs\*.*

	RegDll "$INSTDIR\ActiveDiatheke.ocx"
	
	CreateDirectory "$SMPROGRAMS\${MUI_PRODUCT}"
	CreateShortCut "$SMPROGRAMS\Dreambeam\Dreambeam.lnk" "$INSTDIR\Dreambeam.exe"
	CreateShortCut "$DESKTOP\Dreambeam.lnk" "$INSTDIR\Dreambeam.exe"

	
  ;Store installation folder
  WriteRegStr HKLM "Software\${MUI_PRODUCT}" "" $INSTDIR
  
	; write uninstall strings
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Dreambeam" "DisplayName" "Dreambeam (remove only)"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Dreambeam" "UninstallString" '"$INSTDIR\Uninstall.exe"'
	
  ;Create uninstaller
  WriteUninstaller "$INSTDIR\Uninstall.exe"

SectionEnd



;--------------------------------
;Descriptions

  ;Language strings
  LangString DESC_SecDummy ${LANG_ENGLISH} "A test section."

  ;Assign language strings to sections
  !insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
    !insertmacro MUI_DESCRIPTION_TEXT ${SecDummy} $(DESC_SecDummy)
  !insertmacro MUI_FUNCTION_DESCRIPTION_END
 
;--------------------------------
;Uninstaller Section
UninstallText "This will uninstall ${MUI_PRODUCT}. Hit next to continue."
Section "Uninstall"

  ;Delete Dreambeam Files

	 Delete "$INSTDIR\*.*"
	 RMDir /r "$INSTDIR\de-DE"
		

	DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Dreambeam"

	MessageBox MB_YESNO|MB_ICONQUESTION "Would you like to remove all your Songs and MediaLists?" IDNO NoSongDelete   
    RMDir /r "$INSTDIR\Songs" ; skipped if no
		RMDir /r "$INSTDIR\MediaLists" ; skipped if no		
  NoSongDelete:	

	MessageBox MB_YESNO|MB_ICONQUESTION "Would you like to remove all your Backgrounds?" IDNO NoBGDelete   
    RMDir /r "$INSTDIR\Backgrounds" ; skipped if no				
  NoBGDelete:		
	

	
	;Delete Startmenu Entries
	RMDir	/r "$SMPROGRAMS\${MUI_PRODUCT}"		
  DeleteRegKey /ifempty HKLM "Software\${MUI_PRODUCT}"

	;Delete Uninstaller
	Delete "$INSTDIR\Uninstall.exe"
SectionEnd



	


;--------------------------------
; The stuff to install

!macro Checker
!ifdef FullInstall
	!insertmacro CheckerFull
!else
	!insertmacro CheckerWeb
!endif
!macroend


!macro CheckerWeb
Section "-hidden CheckDependencies"

; .NET 1.0
!define NET1_URL "http://download.microsoft.com/download/a/a/c/aac39226-8825-44ce-90e3-bf8203e74006/dotnetfx.exe"
; .NET 2.0
!define NET_URL "http://download.microsoft.com/download/5/6/7/567758a3-759e-473e-bf8f-52154438565a/dotnetfx.exe"

!define DX_URL "http://download.microsoft.com/download/8/1/e/81ed90eb-dd87-4a23-aedc-298a9603b4e4/directx_9c_redist.exe"
!define DX_MANAGED_URL "http://download.microsoft.com/download/3/9/7/3972f80c-5711-4e14-9483-959d48a2d03b/directx_apr2006_redist.exe"

!define SWORD_URL "http://crosswire.org/ftpmirror/pub/sword/frontend/win32/v1.5/sword-starter-win32-1.5.9.exe"


;=========================================================================================
;== WebInstall =============== Microsof .Net Framework ===================================
;=========================================================================================

	;DotNet:
	;MessageBox MB_OK "Checking for .NET"
	IfFileExists "$WINDIR\Microsoft.NET\Framework\v2.0.50727\InstallUtil.exe" FinishDotNet
		MessageBox MB_YESNOCANCEL|MB_ICONEXCLAMATION \
			"Microsoft .NET Framework 2.0 is required by Dreambeam. \
			$\n$\n Allow Setup to download and install the Framework? (~23 MB)" \
			/SD IDYES IDCANCEL AbortInstall IDNO FinishDotNet

		;================ DOWNLOAD ==================
		NSISdl::download /TIMEOUT=30000 ${NET_URL} "$TEMP\dotnetfx.exe"	
			Pop $0 ;Get the return value		
			StrCmp $0 "success" InstallNet 0
			StrCmp $0 "cancel" 0 +3
			Push "Download cancelled."
			Goto ExitInstall
			Push "Error during download: $0"
			Goto ExitInstall

		;================ Install ==================	
		InstallNet:
		Banner::show /NOUNLOAD /set 76 "Installing Microsoft .Net Framework." \
			"Please wait. This is slow!" \
		ExecWait '"$TEMP\dotnetfx.exe" /q:a /c:"install /q"'
		Delete "$TEMP\dotnetfx.exe"
		Banner::destroy
		
	FinishDotNet:

;=========================================================================================
;== WebInstall ================ Microsoft DirectX  =======================================
; Don't bother with 9.0. Go straight for the apr2006 version which includes managed extensions
;=========================================================================================
;	DirectX:
	;MessageBox MB_OK "Checking for DirectX 9.0 (or later)"
	Goto ManagedDirectX
;	Call GetDXVersion
;	Pop $R3
;	IntCmp $R3 900 FinishDirectX 0 FinishDirectX
;	MessageBox MB_YESNOCANCEL|MB_ICONEXCLAMATION \
;		"Dreambeam requires Microsoft DirectX 9.0 or later. \
;		$\n$\n Allow Setup to download and install DirectX? (~35 MB)" \
;		/SD IDYES IDCANCEL AbortInstall IDNO FinishDirectX
;					
;			;================ DOWNLOAD ==================
;			NSISdl::download /TIMEOUT=30000 ${DX_URL} "$TEMP\directx_9c_redist.exe"	
;			Pop $0 ;Get the return value
;			StrCmp $0 "success" InstallDX 0
;			StrCmp $0 "cancel" 0 +3
;			Push "Download cancelled."
;			Goto ExitInstall
;			Push "Unkown error during download."
;			Goto ExitInstall
;		;================ Install ==================	
;		InstallDX:		
;		Banner::show /NOUNLOAD /set 76 "Installing Microsoft DirectX." \
;			"Please wait ..."
;		ExecWait '"$TEMP\directx_9c_redist.exe" /q:a /t:"$TEMP\DirectX"'	
;		ExecWait '"$TEMP\DirectX\dxsetup.exe" /silent'	
;		Delete "$TEMP\DirectX\*.*"
;		Delete "$TEMP\directx_9c_redist.exe"
;		Banner::destroy
;
;	FinishDirectX:
	
;=========================================================================================
;== WebInstall ================ Microsoft DirectX Managed  ===============================
;=========================================================================================
	ManagedDirectX:
	;MessageBox MB_OK "Checking for DirectX Managed"
	IfFileExists "$WINDIR\assembly\GAC\Microsoft.DirectX" FinishManagedDirectX
	;MessageBox MB_OK "Did not find the managed wrappers for DirectX"
	MessageBox MB_YESNOCANCEL|MB_ICONEXCLAMATION \
		"Dreambeam requires Microsoft DirectX Managed extensions. \
		$\n$\n Allow Setup to download and install DirectX with Managed extensions? (~53 MB)" \
		/SD IDYES IDCANCEL AbortInstall IDNO FinishManagedDirectX

		!define directx_file "directx_redist.exe"

                ;================ DOWNLOAD ==================
		NSISdl::download /TIMEOUT=30000 ${DX_MANAGED_URL} "$TEMP\${directx_file}"
		Pop $0 ;Get the return value
		StrCmp $0 "success" InstallDXManaged 0
		StrCmp $0 "cancel" 0 +3
		Push "Download cancelled."
		Goto ExitInstall
		Push "Unkown error during download."
		Goto ExitInstall
			
		;================ Install ==================
		InstallDXManaged:
		Banner::show /NOUNLOAD /set 76 "Installing Microsoft DirectX." \
			"Please wait. This is SLOOOOW."
		ExecWait '"$TEMP\${directx_file}" /q:a /t:"$TEMP\DirectXManaged"'
		ExecWait '"$TEMP\DirectXManaged\dxsetup.exe" /silent'

		RMDir /r "$TEMP\DirectXManaged"
		Delete "$TEMP\${directx_file}"
		Banner::destroy

	FinishManagedDirectX:

;=========================================================================================
;== WebInstall ================ The SWORD Project ========================================
;=========================================================================================
	Sword:
	;MessageBox MB_OK "Checking for The SWORD Project"
	IfFileExists "$PROGRAMFILES\CrossWire\The SWORD Project\sword.exe" FinishSword
	;MessageBox MB_OK "Did not find The SWORD Project"
	MessageBox MB_YESNO|MB_ICONEXCLAMATION \
		"The Sword program was not found in the default install directory.\
		$\n$\n Allow Setup to download and install it? (~7Mb)\
		$\n If you installed it somewhere else, say No." \
		/SD IDYES IDNO FinishSword IDYES DownloadSword

                ;================ DOWNLOAD ==================
		DownloadSword:
		!define sword_file "sword-starter-win32-1.5.9.exe"
		NSISdl::download /TIMEOUT=30000 ${SWORD_URL} "$TEMP\${sword_file}"
			Pop $0 ;Get the return value
			StrCmp $0 "success" InstallSword 0
			StrCmp $0 "cancel" 0 +3
			Push "Download cancelled."
			Goto DownloadErrorSword
			Push "Unkown error during download."
			Goto DownloadErrorSword
			
		;================ Install ==================
		InstallSword:
		MessageBox MB_OK "Please close the windows that open up\
			$\n as a result of installing SWORD so that\
			$\n DreamBeam can continue with the install."
		Banner::show /NOUNLOAD /set 76 "Installing The SWORD Project." \
			"Please be patient..."
		ExecWait "$TEMP\${sword_file} /S"
		Delete "$TEMP\${sword_file}"
		Banner::destroy
		Goto FinishSword

	DownloadErrorSword:
		Pop $2
		MessageBox MB_OK "The install of The SWORD Project was interrupted for the following reason: $2"
		Goto FinishSword

	FinishSword:

;=========================================================================================
;== WebInstall ================ Macromedia Flash Player ==================================
;=========================================================================================
	Flash:
	Push "flash.ocx"
	Call "GetFlashVER"
	Pop $1
	;MessageBox MB_OK "The flash version was: $1"
	IntCmp $1 2 FinishFlash 0 FinishFlash
	MessageBox MB_YESNOCANCEL|MB_ICONEXCLAMATION \
		"Dreambeam requires Macromedia Flash Player 7 or later. \
		$\n$\n Allow Setup to install the Flash player?" \
		/SD IDYES IDCANCEL AbortInstall IDNO NextStep

	!define flash_file "flashplayer7_winax.exe"

	File /oname=$TEMP\${flash_file} SupportFiles\${flash_file}
	Banner::show /NOUNLOAD /set 76 "Installing Macromedia Flash." "Please wait..."						
	ExecWait '"$TEMP\${flash_file}" /Q'	
	Banner::destroy	

	Delete "$TEMP\${flash_file}"

	FinishFlash:

;=========================================================================================
;== WebInstall ================ Exit points for the installer ============================
;=========================================================================================
	Goto NextStep	

	AbortInstall:
	        MessageBox MB_OK "The installer has been aborted. \
		$\n$\n Please run it again to install DreamBeam" /SD IDOK
		Quit
	
	ExitInstall: 
		Pop $2
		MessageBox MB_OK "The setup is about to be interrupted for the following reason: $2"
  		Quit
	
	NextStep:

SectionEnd ; end the section
!macroend

;=========================================================================================
;=========================================================================================
;============================ Full (not web) Install =====================================
;=========================================================================================
;=========================================================================================


!macro CheckerFull
Section "-hidden CheckDependencies" ;

;=========================================================================================
;== FullInstall ============== Microsof .Net Framework ===================================
;=========================================================================================
	DotNet:
	IfFileExists "$WINDIR\Microsoft.NET\Framework\v2.0.50727\InstallUtil.exe" FinishDotNet

		MessageBox MB_YESNOCANCEL|MB_ICONEXCLAMATION \
			"Microsoft .NET Framework 2.0 is required by Dreambeam. \
			$\n$\n Allow Setup to install the Framework?" \
			/SD IDYES IDCANCEL AbortInstall IDNO FinishDotNet

		SetOutPath "$TEMP\dotNet2.0"
		File /x content.txt "SupportFiles\dotNet2.0\*.*"
		Banner::show /NOUNLOAD /set 76 "Installing Microsoft .Net Framework." \
			"Please wait. This is SLOOOOW."
		ExecWait '"$TEMP\dotNet2.0\dotnetfx.exe" /q:a /c:"install /q"'
		RMDir /r "$TEMP\dotNet2.0"
		Banner::destroy	

	FinishDotNet:

;=========================================================================================
;== FullInstall =============== Microsoft DirectX  =======================================
;======== Skip over 9c, and go directly to the managed version of DirectX ================
;=========================================================================================
;	DirectX9c:
;	Goto ManagedDirectX
;	Call GetDXVersion
;	Pop $R3
;	IntCmp $R3 900 FinishDirectX9C 0 FinishDirectX9C
; 	MessageBox MB_OKCANCEL|MB_ICONEXCLAMATION \
;		"Dreambeam requires Microsoft DirectX 9.0 or later. \
;		$\n$\n Setup will now install DirectX." \
;		/SD IDOK IDCANCEL AbortInstall
;
;		SetOutPath "$TEMP\Directx9c"
;		File /x content.txt SupportFiles\DirectX9c\*.*
;
;		Banner::show /NOUNLOAD /set 76 "Installing Microsoft DirectX." "Please have some patience..."				
;		;ExecWait '"$TEMP\directx_9c_redist.exe" /q:a /t:"$TEMP"'	
;		ExecWait '"$TEMP\DirectX9c\dxsetup.exe" /silent'	
;		Banner::destroy
;
;		RMDir /r "$TEMP\DirectX9c"
;
;	FinishDirectX9c:


;=========================================================================================
;== FullInstall =============== Microsoft DirectX Managed  ===============================
;=========================================================================================
	ManagedDirectX:
	;MessageBox MB_OK "Checking for DirectX Managed"
	
	; See if the directory exists ...
	IfFileExists "$WINDIR\assembly\GAC\Microsoft.DirectX\*.*" FinishManagedDirectX
	;MessageBox MB_OK "Did not find the managed wrappers for DirectX"
	
	MessageBox MB_YESNOCANCEL|MB_ICONEXCLAMATION \
		"Dreambeam requires Microsoft DirectX 9.0 or later with managed extensions. \
		$\n$\n Allow Setup to install DirectX?" \
		/SD IDYES IDCANCEL AbortInstall IDNO FinishManagedDirectX

		!define directx_file "directx_apr2006_redist.exe"
		
		SetOutPath "$TEMP"
		File /x content.txt SupportFiles\${directx_file}
		
		Banner::show /NOUNLOAD /set 76 "Extracting Microsoft DirectX." ""
		;MessageBox MB_OK "Copied directx"
		ExecWait '"$TEMP\${directx_file}" /q:a /t:"$TEMP\DirectXManaged"'
		Banner::destroy
		;MessageBox MB_OK "Extracted DirectX Managed"
		
		Banner::show /NOUNLOAD /set 76 "Installing Microsoft DirectX." \
			"Please wait. This is SLOOOOW."
		ExecWait '"$TEMP\DirectXManaged\dxsetup.exe" /silent'
		Banner::destroy
		;MessageBox MB_OK "Installed directx managed"

		RMDir /r "$TEMP\DirectXManaged"
		Delete "$TEMP\${directx_file}"

	FinishManagedDirectX:

;=========================================================================================
;== FullInstall ================= The SWORD Project ======================================
;=========================================================================================

	Sword:
;	MessageBox MB_OK "Checking for Sword"
	IfFileExists "$PROGRAMFILES\CrossWire\The SWORD Project\sword.exe" FinishSword
	MessageBox MB_YESNO|MB_ICONEXCLAMATION \
		"The Sword program was not found in the default install directory.\
		$\n$\n Should we install the version included in this installer? \
		$\n If you installed it somewhere else, say No." \
		/SD IDYES IDNO FinishSword IDYES InstallSword

	InstallSword:
		!define sword_file "sword-starter-win32-1.5.9.exe"
		;MessageBox MB_OK "Installing Sword ${sword_file}"

		SetOutPath "$TEMP"
		File "SupportFiles\${sword_file}"

		MessageBox MB_OK "Please close the windows that open up\
			$\n as a result of installing SWORD so that\
			$\n DreamBeam can continue with the install."

		Banner::show /NOUNLOAD /set 76 "Installing SWORD" ""
		ExecWait "$TEMP\${sword_file} /S"
		Banner::destroy
		
		Delete "$TEMP\${sword_file}"

	FinishSword:



;=========================================================================================
;== FullInstall =============== Macromedia Flash Player ==================================
;=========================================================================================
	Flash:
	Push "flash.ocx"
	Call "GetFlashVER"
	Pop $1
	;MessageBox MB_OK "The flash version was: $1"
	IntCmp $1 2 FinishFlash 0 FinishFlash
	MessageBox MB_YESNOCANCEL|MB_ICONEXCLAMATION \
		"Dreambeam requires Macromedia Flash Player 7 or later. \
		$\n$\n Allow Setup to install the Flash player?" \
		/SD IDYES IDCANCEL AbortInstall IDNO NextStep

	!define flash_file "flashplayer7_winax.exe"

	File /oname=$TEMP\${flash_file} SupportFiles\${flash_file}
	Banner::show /NOUNLOAD /set 76 "Installing Macromedia Flash." "Please wait..."						
	ExecWait '"$TEMP\${flash_file}" /Q'	
	Banner::destroy	

	Delete "$TEMP\${flash_file}"
	
	FinishFlash:

;=========================================================================================
;== FullInstall =============== Exit points for the installer ============================
;=========================================================================================
	Goto NextStep	

	AbortInstall:
	        MessageBox MB_OK "The installer has been aborted. \
		$\n$\n Please run it again to install DreamBeam" /SD IDOK
		Quit

	NextStep:

SectionEnd ; end the section
!macroend

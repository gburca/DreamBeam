
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

!define NET1_URL "http://download.microsoft.com/download/a/a/c/aac39226-8825-44ce-90e3-bf8203e74006/dotnetfx.exe"
!define NET_URL "http://www.microsoft.com/downloads/details.aspx?familyid=0856eacb-4362-4b0d-8edd-aab15c5e04f5&displaylang=en"
!define DX_URL "http://download.microsoft.com/download/8/1/e/81ed90eb-dd87-4a23-aedc-298a9603b4e4/directx_9c_redist.exe"
!define DX_MANAGED_URL "http://download.microsoft.com/download/3/9/7/3972f80c-5711-4e14-9483-959d48a2d03b/directx_apr2006_redist.exe"
!define SWORD_URL "http://crosswire.org/ftpmirror/pub/sword/frontend/win32/v1.5/sword-starter-win32-1.5.6.exe"


;=========================================================================================
;============================= Microsof .Net Framework ===================================
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
			Push "Unkown error during download."
			Goto ExitInstall

		;================ Install ==================	
		InstallNet:
		Banner::show /NOUNLOAD /set 76 "Installing Microsoft .Net Framework." \
			"Please wait ..."
		ExecWait '"$TEMP\dotnetfx.exe" /q:a /c:"install /q"'
		Delete "$TEMP\dotnetfx.exe"
		Banner::destroy
		
	FinishDotNet:

;=========================================================================================
;============================== Microsoft DirectX  =======================================
;=========================================================================================
	;DirectX:
	;MessageBox MB_OK "Checking for DirectX 9.0 (or later)"
	Goto ManagedDirectX     ; Don't bother with 9.0. Go straight for the apr2006 version which includes managed extensions
	Call GetDXVersion
	Pop $R3
	IntCmp $R3 900 FinishDirectX 0 FinishDirectX
	MessageBox MB_YESNOCANCEL|MB_ICONEXCLAMATION \
		"Dreambeam requires Microsoft DirectX 9.0 or later. \
		$\n$\n Allow Setup to download and install DirectX? (~35 MB)" \
		/SD IDYES IDCANCEL AbortInstall IDNO FinishDirectX
					
			;================ DOWNLOAD ==================
			NSISdl::download /TIMEOUT=30000 ${DX_URL} "$TEMP\directx_9c_redist.exe"	
			Pop $0 ;Get the return value
			StrCmp $0 "success" InstallDX 0
			StrCmp $0 "cancel" 0 +3
			Push "Download cancelled."
			Goto ExitInstall
			Push "Unkown error during download."
			Goto ExitInstall
		;================ Install ==================	
		InstallDX:		
		Banner::show /NOUNLOAD /set 76 "Installing Microsoft DirectX." \
			"Please wait ..."
		ExecWait '"$TEMP\directx_9c_redist.exe" /q:a /t:"$TEMP\DirectX"'	
		ExecWait '"$TEMP\DirectX\dxsetup.exe" /silent'	
		Delete "$TEMP\DirectX\*.*"
		Delete "$TEMP\directx_9c_redist.exe"
		Banner::destroy

	FinishDirectX:
	
;=========================================================================================
;============================== Microsoft DirectX Managed  ===============================
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
			"Please be patient, it is very slow..."
		ExecWait '"$TEMP\${directx_file}" /q:a /t:"$TEMP\DirectXManaged"'
		ExecWait '"$TEMP\DirectXManaged\dxsetup.exe" /silent'

		RMDir /r "$TEMP\DirectXManaged"
		Delete "$TEMP\${directx_file}"
		Banner::destroy

	FinishManagedDirectX:

;=========================================================================================
;============================== Macromedia Flash Player ==================================
;=========================================================================================
	Flash:
	Push "flash.ocx"
	Call "GetFlashVER"
	Pop $1
	IntCmp $1 2 0 NextStep 0
	MessageBox MB_YESNOCANCEL|MB_ICONEXCLAMATION \
		"Dreambeam requires Macromedia Flash Player 7 or later. \
		$\n$\n Allow Setup to install the Flash player?" \
		/SD IDYES IDCANCEL AbortInstall IDNO NextStep

	!define flash_file "flashplayer7_winax.exe"

	File /oname=$TEMP\${flash_file} SupportFiles\${flash_file}
	Banner::show /NOUNLOAD /set 76 "Installing Macromedia Flash." "Please have some patience..."						
	ExecWait '"$TEMP\${flash_file}" /Q'	
	Banner::destroy	

	Delete "$TEMP\${flash_file}"

	Goto NextStep	

	AbortInstall:
	        MessageBox MB_OK "The installer has been aborted. \
		$\n$\n Please run it again to install DreamBeam" /SD IDOK
		Quit
	
	ExitInstall: 
		Pop $2
		MessageBox MB_OK "The setup is about to be interrupted for the following reason : $2"
  		Quit
	
	NextStep:

SectionEnd ; end the section
!macroend



!macro CheckerFull
Section "-hidden CheckDependencies" ;

;=========================================================================================
;============================= Microsof .Net Framework ===================================
;=========================================================================================
	DotNet:
	IfFileExists "$WINDIR\Microsoft.NET\Framework\v2.0.50727\InstallUtil.exe" FinishDotNet

		MessageBox MB_YESNOCANCEL|MB_ICONEXCLAMATION \
			"Microsoft .NET Framework 2.0 is required by Dreambeam. \
			$\n$\n Allow Setup to install the Framework?" \
			/SD IDYES IDCANCEL AbortInstall IDNO FinishDotNet

		SetOutPath "$TEMP\dotNet2.0"
		File /x content.txt "SupportFiles\dotNet2.0\*.*"
		Banner::show /NOUNLOAD /set 76 "Installing Microsoft .Net Framework." "Please wait ..."
		ExecWait '"$TEMP\dotNet2.0\dotnetfx.exe" /q:a /c:"install /q"'
		RMDir /r "$TEMP\dotNet2.0"
		Banner::destroy	

	FinishDotNet:

;=========================================================================================
;============================== Microsoft DirectX  =======================================
;=========================================================================================
;	DirectX9c:
;	Goto ManagedDirectX	; Skip over 9c, and go directly to the managed version of DirectX
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
;============================== Microsoft DirectX Managed  ===============================
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
			"Please have some patience... $\n$\n This takes a very loooooong time to install."
		ExecWait '"$TEMP\DirectXManaged\dxsetup.exe" /silent'
		Banner::destroy
		;MessageBox MB_OK "Installed directx managed"

		RMDir /r "$TEMP\DirectXManaged"
		Delete "$TEMP\${directx_file}"

	FinishManagedDirectX:

;=========================================================================================
;================================ The SWORD Project ======================================
;=========================================================================================

;	Sword:
;	MessageBox MB_OK "Checking for Sword"
;	IfFileExists "$PROGRAMFILES\CrossWire\The SWORD Project\sword.exe" FinishSword
;	MessageBox MB_YESNO|MB_ICONEXCLAMATION \
;		"The Sword program was not found in the default install directory.\
;		$\n$\n Should we try to download and install it? \
;		$\n If you installed it somewhere else, say No." \
;		/SD IDYES IDNO FinishSword IDYES InstallSword
;
;	InstallSword:
;		!define sword_file "sword-win32-1.5.6.exe"
;		;MessageBox MB_OK "Installing Sword ${sword_file}"
;
;		SetOutPath "$TEMP"
;		File "SupportFiles\${sword_file}"
;
;		Banner::show /NOUNLOAD /set 76 "Installing SWORD" ""
;		ExecWait "$TEMP\${sword_file} /S"
;		Banner::destroy
;		
;		Delete "$TEMP\${sword_file}"
;
;	FinishSword:



;=========================================================================================
;============================== Macromedia Flash Player ==================================
;=========================================================================================
	Flash:
	Push "flash.ocx"
	Call "GetFlashVER"
	Pop $1
	IntCmp $1 2 0 NextStep 0
	MessageBox MB_YESNOCANCEL|MB_ICONEXCLAMATION \
		"Dreambeam requires Macromedia Flash Player 7 or later. \
		$\n$\n Allow Setup to install the Flash player?" \
		/SD IDYES IDCANCEL AbortInstall IDNO NextStep

	!define flash_file "flashplayer7_winax.exe"

	File /oname=$TEMP\${flash_file} SupportFiles\${flash_file}
	Banner::show /NOUNLOAD /set 76 "Installing Macromedia Flash." "Please have some patience..."						
	ExecWait '"$TEMP\${flash_file}" /Q'	
	Banner::destroy	

	Delete "$TEMP\${flash_file}"
	
	Goto NextStep	

	AbortInstall:
	        MessageBox MB_OK "The installer has been aborted. \
		$\n$\n Please run it again to install DreamBeam" /SD IDOK
		Quit

	NextStep:

SectionEnd ; end the section
!macroend

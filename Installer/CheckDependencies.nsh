
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
Section "CheckDependencies" ;

;=========================================================================================
;============================= Microsof .Net Framework ===================================
;=========================================================================================
!define NET_URL "http://download.microsoft.com/download/a/a/c/aac39226-8825-44ce-90e3-bf8203e74006/dotnetfx.exe"	
!define DX_URL "http://download.microsoft.com/download/8/1/e/81ed90eb-dd87-4a23-aedc-298a9603b4e4/directx_9c_redist.exe"	
	
	DotNet:
	IfFileExists "$WINDIR\Microsoft.NET\Framework\v1.1.4322\installUtil.exe" NextStep 
		MessageBox MB_OKCANCEL|MB_ICONEXCLAMATION "Microsoft .NET Framework 1.1 is required by Dreambeam. $\n$\n Setup will now try to download and install the Framework. (~23 MB)" /SD IDOK IDCANCEL false		  
		
		
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
		Banner::show /NOUNLOAD /set 76 "Installing Microsoft .Net Framework." "Please have some patience..."				
		ExecWait '"$TEMP\dotnetfx.exe" /q:a /c:"install /q"'	
		Delete "$TEMP\dotnetfx.exe"
		Banner::destroy	

;=========================================================================================
;============================== Microsoft DirectX  =======================================
;=========================================================================================
	DirectX:
	Call GetDXVersion
	Pop $R3
  IntCmp $R3 900 NextStep 0 NextStep
    MessageBox MB_OKCANCEL|MB_ICONEXCLAMATION "Dreambeam requires Microsoft DirectX 9.0 or later. $\n$\n Setup will now try to download and install DirectX. (~35 MB)" /SD IDOK IDCANCEL false    
					
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
		Banner::show /NOUNLOAD /set 76 "Installing Microsoft DirectX." "Please have some patience..."				
		ExecWait '"$TEMP\directx_9c_redist.exe" /q:a /t:"$TEMP\DirectX"'	
		ExecWait '"$TEMP\DirectX\dxsetup.exe" /silent'	
		Delete "$TEMP\DirectX\*.*"
		Delete "$TEMP\directx_9c_redist.exe"
		Banner::destroy
		
	
;=========================================================================================
;============================== Macromedia Flash Player ==================================
;=========================================================================================
	Push "flash.ocx"
	Call "GetFlashVER"
	Pop $1
  IntCmp $1 2 0 NextStep 0
	;MessageBox MB_OKCANCEL|MB_ICONEXCLAMATION "Dreambeam requires Macromedia Flash Player 7 or later. $\n$\n Setup will now install the Flash player." /SD IDOK IDCANCEL false	
	File /oname=$TEMP\flashplayer7_winax.exe SupportFiles\flashplayer7_winax.exe
	Banner::show /NOUNLOAD /set 76 "Installing Macromedia Flash." "Please have some patience..."						
	ExecWait '"$TEMP\flashplayer7_winax.exe" /Q'	
	Banner::destroy	
	
	
	Goto NextStep	
	false:
	Quit
	
	ExitInstall: 
	Pop $2
  MessageBox MB_OK "The setup is about to be interrupted for the following reason : $2"
  Quit
	
	NextStep:	
SectionEnd ; end the section
!macroend



!macro CheckerFull
Section "CheckDependencies" ;

;=========================================================================================
;============================= Microsof .Net Framework ===================================
;=========================================================================================
	DotNet:
	IfFileExists "$WINDIR\Microsoft.NET\Framework\v1.1.4322\installUtil.exe" NextStep 
		MessageBox MB_OKCANCEL|MB_ICONEXCLAMATION "Microsoft .NET Framework 1.1 is required by Dreambeam. $\n$\n Setup will now install the Framework." /SD IDOK IDCANCEL false		  
		;File /oname=$TEMP\dotnetfx.exe SupportFiles\dotnetfx.exe		
		SetOutPath "$TEMP\dotNet1.1"
		File SupportFiles\dotNet1.1\*.*
		Banner::show /NOUNLOAD /set 76 "Installing Microsoft .Net Framework." "Please have some patience..."				
		ExecWait '"$TEMP\dotNet1.1\install.exe" /q'	
		Delete "$TEMP\dotNet1.1\*.*"
		Banner::destroy	

;=========================================================================================
;============================== Microsoft DirectX  =======================================
;=========================================================================================
	DirectX:
	Call GetDXVersion
	Pop $R3
  IntCmp $R3 900 NextStep 0 NextStep
    MessageBox MB_OKCANCEL|MB_ICONEXCLAMATION "Dreambeam requires Microsoft DirectX 9.0 or later. $\n$\n Setup will now install DirectX." /SD IDOK IDCANCEL false    
		SetOutPath "$TEMP\directx9c"
		File SupportFiles\DirectX9c\*.*
		Banner::show /NOUNLOAD /set 76 "Installing Microsoft DirectX." "Please have some patience..."				
		;ExecWait '"$TEMP\directx_9c_redist.exe" /q:a /t:"$TEMP"'	
		ExecWait '"$TEMP\DirectX9c\dxsetup.exe" /silent'	
		Delete "$TEMP\DirectX9c\*.*"
		Banner::destroy
		
	
;=========================================================================================
;============================== Macromedia Flash Player ==================================
;=========================================================================================
	Push "flash.ocx"
	Call "GetFlashVER"
	Pop $1
  IntCmp $1 2 0 NextStep 0
	;MessageBox MB_OKCANCEL|MB_ICONEXCLAMATION "Dreambeam requires Macromedia Flash Player 7 or later. $\n$\n Setup will now install the Flash player." /SD IDOK IDCANCEL false	
	File /oname=$TEMP\flashplayer7_winax.exe SupportFiles\flashplayer7_winax.exe
	Banner::show /NOUNLOAD /set 76 "Installing Macromedia Flash." "Please have some patience..."						
	ExecWait '"$TEMP\flashplayer7_winax.exe" /Q'	
	Banner::destroy	
	
	
	Goto NextStep	
	false:
	Quit
	NextStep:	
SectionEnd ; end the section
!macroend

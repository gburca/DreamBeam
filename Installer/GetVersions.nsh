
  Function GetDXVersion
    Push $0
    Push $1

    ReadRegStr $0 HKLM "Software\Microsoft\DirectX" "Version"
    IfErrors noDirectX

    StrCpy $1 $0 2 5    ; get the minor version
    StrCpy $0 $0 2 2    ; get the major version
    IntOp $0 $0 * 100   ; $0 = major * 100 + minor
    IntOp $0 $0 + $1
    Goto done

    noDirectX:
      StrCpy $0 0
    
    done:
      Pop $1
      Exch $0
  FunctionEnd
	
	
	
Function "GetFlashVER"
	Exch $0
	GetDllVersion "$SYSDIR\Macromed\Flash\$0" $R0 $R1
	IntOp $R2 $R0 / 0x00010000
	IntOp $R3 $R0 & 0x0000FFFF
	IntOp $R4 $R1 / 0x00010000
	IntOp $R5 $R1 & 0x0000FFFF
	StrCmp $R2 "0" done 0
	StrCpy $1 "$R2.$R3.$R4.$R5"
	Goto +2
	StrCpy $1 "No file version"
	done:	
	${VersionCheck} $1 "7.0.19.0" $1	
	Exch $1	
FunctionEnd
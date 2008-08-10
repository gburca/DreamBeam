For developers:

- The files found in the "___DLLs for System32___" directory must be copied to
  the Windows/System32 directory. OPaC.uxTheme.Win32.dll depends on msvcr70.dll
  being in the System32 directory as well. It will NOT load properly if another
  version of msvcr is present instead (such as msvcr71.dll).

- Unless you already installed DreamBeam as a user, you will need to register
  ActiveDiatheke.ocx by executing the "__RUN ME TO AVOID PROBLEMS.cmd" file.

- The DirectX SDK is required. Download it from the usual place.

- For some reason, the very first build fails with an error on Options.resx.
  Subsequent builds are fine.

- DreamBeam depends on a number of libraries. The source code for those
  libraries is maintained at ebixio.com in separate repositories from
  DreamBeam. Visual Studio expects to find the source code for those libraries
  in the following directory structure:

	DreamBeam
	ExRichTextBox
	MessageBoxExLib
	MyControlSamples

- The contents of the non-DreamBeam directories can be obtained from:

	- MyControlSamples
		https://ebixio.com/svn/ControlLib
	- ExRichTextBox
		https://ebixio.com/svn/ExRichTextBox/branches/DreamBeam
	- MessageBoxExLib
		https://ebixio.com/svn/MessageBoxExLib/DreamBeam

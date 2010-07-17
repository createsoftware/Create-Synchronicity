#This file is part of Create Synchronicity.
#
#Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
#Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
#You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
#Created by:	Clément Pit--Claudel.
#Web site:		http://synchronicity.sourceforge.net.

!include MUI2.nsh
!define	/file	VERSION		"..\..\..\..\..\..\Sites Web\Sourceforge\Synchronicity\code\version.txt"

!define 		COMPANY		"Create Software"
!define 		PRODUCTNAME	"Create Synchronicity"

!define 		REGPATH		"Software\${COMPANY}"
!define 		SUBREGPATH	"${REGPATH}\${PRODUCTNAME}"

!define 		COMPANYPATH	"$PROGRAMFILES\${COMPANY}"
!define 		PROGRAMPATH	"${COMPANYPATH}\${PRODUCTNAME}"
!define			PRODUCTPATH	"${COMPANY}\${PRODUCTNAME}"

SetCompressor /SOLID lzma

Name "${PRODUCTNAME} ${VERSION}"
OutFile "..\Create_Synchronicity_Setup.exe"
InstallDir "${PROGRAMPATH}"
InstallDirRegKey HKLM "${SUBREGPATH}" "InstallPath"

RequestExecutionLevel admin
Var StartMenuFolder

!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_DIRECTORY

!define MUI_STARTMENUPAGE_REGISTRY_ROOT			"HKCU"
!define MUI_STARTMENUPAGE_REGISTRY_KEY			"${SUBREGPATH}"
!define MUI_STARTMENUPAGE_REGISTRY_VALUENAME	"StartMenuFolder"
!define MUI_STARTMENUPAGE_DEFAULTFOLDER			"${PRODUCTPATH}"
!insertmacro MUI_PAGE_STARTMENU AppStartMenu $StartMenuFolder

!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH

!insertmacro MUI_UNPAGE_WELCOME
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES
!insertmacro MUI_UNPAGE_FINISH

!insertmacro MUI_LANGUAGE "English"

!macro ExitIfRunning UN
Function ${UN}.onInit
	Beginning:
		FindProcDLL::FindProc "Create Synchronicity.exe"
		IntCmp $R0 0 OkCase
			MessageBox MB_ABORTRETRYIGNORE|MB_ICONEXCLAMATION "Create Synchronicity is running. Please close it before continuing." IDABORT AbortCase IDRETRY RetryCase
				Goto OkCase
		
	AbortCase:
		Abort
	
	RetryCase:
		Goto Beginning
	
	OkCase:
FunctionEnd
!macroend

!insertmacro ExitIfRunning ""
!insertmacro ExitIfRunning "un"

Section "Installer Section" InstallSection
	SetOutPath $INSTDIR

	File "bin\Release\Create Synchronicity.exe"
	File "bin\Release\Release notes.txt"
	File "bin\Release\COPYING"

	SetOutPath "$INSTDIR\languages"
	File "bin\Release\languages\*.lng"

	!insertmacro MUI_STARTMENU_WRITE_BEGIN AppStartMenu
	CreateDirectory "$SMPROGRAMS\$StartMenuFolder"
	CreateShortCut "$SMPROGRAMS\$StartMenuFolder\${PRODUCTNAME}.lnk" "$INSTDIR\${PRODUCTNAME}.exe"
	CreateShortCut "$SMPROGRAMS\$StartMenuFolder\Uninstall.lnk" "$INSTDIR\Uninstall.exe"
	!insertmacro MUI_STARTMENU_WRITE_END

	WriteRegStr HKLM "${SUBREGPATH}" "InstallPath" $INSTDIR
	WriteUninstaller "$INSTDIR\Uninstall.exe"
SectionEnd

Section "Uninstall"
	Delete "$INSTDIR\Create Synchronicity.exe"
	Delete "$INSTDIR\Release notes.txt"
	Delete "$INSTDIR\COPYING"
	Delete "$INSTDIR\Uninstall.exe"

	!insertmacro MUI_STARTMENU_GETFOLDER AppStartMenu $StartMenuFolder

	Delete "$SMPROGRAMS\$StartMenuFolder\${PRODUCTNAME}.lnk"
	Delete "$SMPROGRAMS\$StartMenuFolder\Uninstall.lnk"
	RMDir "$SMPROGRAMS\$StartMenuFolder"
	RMDir "$SMPROGRAMS\${COMPANY}\" #remove the "Create Software" folder if empty

	RMDir /r "$INSTDIR\languages\"
	RMDir /r "$INSTDIR\config\"
	RMDir /r "$INSTDIR\log\"
	RMDir "$INSTDIR\"
	RMDir "${COMPANYPATH}\" #remove the "Create Software" folder if empty

	RMDir /r "$APPDATA\Create Software\Create Synchronicity\"
	RMDir "$APPDATA\Create Software\" #remove the "Create Software" folder if empty

	DeleteRegKey HKLM "${SUBREGPATH}"
	DeleteRegKey /ifempty HKLM "${REGPATH}" #remove the "Create Software" key if empty
SectionEnd

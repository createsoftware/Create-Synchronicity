!include MUI2.nsh
!define	/file	VERSION		"..\..\..\..\..\..\Sites Web\Sourceforge\Synchronicity\code\version.txt"

!define 		COMPANY		"Create Software"
!define 		PRODUCTNAME	"Create Synchronicity"

!define 		REGPATH		"Software\${COMPANY}"
!define 		SUBREGPATH	"${REGPATH}\${PRODUCTNAME}"

!define 		COMPANYPATH	"$PROGRAMFILES\${COMPANY}"
!define 		PROGRAMPATH	"${COMPANYPATH}\${PRODUCTNAME}"
!define			PRODUCTPATH	"${COMPANY}\${PRODUCTNAME}"

Name "${PRODUCTNAME} ${VERSION}"
OutFile "..\Create_Synchronicity_Setup.exe"
InstallDir "${PROGRAMPATH}"
InstallDirRegKey HKLM "${SUBREGPATH}" "InstallPath"

RequestExecutionLevel user
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

Section "Installer Section" InstallSection
	SetOutPath $INSTDIR

	File "bin\Release\Create Synchronicity.exe"
	File "bin\Release\Release notes.txt"
	File "bin\Release\COPYING"

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
	RMDir "$SMPROGRAMS\${COMPANY}" #remove the "Create Software" folder if empty

	RMDir /r "$INSTDIR\config"
	RMDir /r "$INSTDIR\log"
	RMDir "$INSTDIR"
	
	RMDir "${COMPANYPATH}" #remove the "Create Software" folder if empty
	DeleteRegKey HKLM "${SUBREGPATH}"
	DeleteRegKey /ifempty HKLM "${REGPATH}" #remove the "Create Software" key if empty
SectionEnd

@echo OFF
@if "%1" == "/?" goto help

:start
@echo This file is part of Create Synchronicity.
@echo Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
@echo Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
@echo You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see http://www.gnu.org/licenses/.
@echo Created by:   Clément Pit--Claudel.
@echo Web site:     http://synchronicity.sourceforge.net.

@set REV=%1
@set LOG="build\buildlog-r%REV%.txt"
mkdir build

(echo Packaging log for r%REV% & date /t & time /t & echo.) > %LOG%

echo (1/6) Building program (release)
(
"C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE\devenv.exe" "Create Synchronicity.sln" /Rebuild Release /Out %LOG%
) >> %LOG%

echo (2/6) Building program (debug)
(
"C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE\devenv.exe" "Create Synchronicity.sln" /Rebuild Debug /Out %LOG%
) >> %LOG%

echo (3/6) Building installer
(
echo.
echo -----
"C:\Program Files (x86)\NSIS\makensis.exe" "Create Synchronicity\setup_script.nsi"
echo.
echo -----
move Create_Synchronicity_Setup.exe "build\Create_Synchronicity_Setup-r%REV%.exe"
) >> %LOG%

echo (4/6) Building zip files
(
echo.
echo -----
cd "Create Synchronicity\bin\Release"
"C:\Program Files\7-Zip\7z.exe" a "..\..\..\build\Create_Synchronicity-r%REV%.zip" "Create Synchronicity.exe" "Release notes.txt" "COPYING" "languages\*"
cd ..\..\..

cd "Create Synchronicity\bin\Debug"
"C:\Program Files\7-Zip\7z.exe" a "..\..\..\build\Create_Synchronicity-r%REV%-DEBUG.zip" "Create Synchronicity.exe" "Release notes.txt" "COPYING" "languages\*"
cd ..\..\..
) >> %LOG%

echo (5/6) Creating latest.txt
(
echo.
echo -----
	(
	echo Last build r%REV% & date /t & time /t
	echo.
	echo https://sourceforge.net/projects/synchronicity/files/Create Synchronicity/Unreleased (SVN Builds)/Create_Synchronicity-r%REV%.zip/download
	echo https://sourceforge.net/projects/synchronicity/files/Create Synchronicity/Unreleased (SVN Builds)/Create_Synchronicity_Setup-r%REV%.exe/download 
	echo https://sourceforge.net/projects/synchronicity/files/Create Synchronicity/Unreleased (SVN Builds)/Create_Synchronicity-r%REV%-DEBUG.zip/download 
	) > build\latest.txt
) >> %LOG%

echo (6/6) Uploading build to frs.sourceforge.net
(
echo.
echo -----
echo Uploading files via SCP.
"C:\Program Files (x86)\PuTTY\pscp.exe" "build\Create_Synchronicity-r%REV%.zip" "build\Create_Synchronicity-r%REV%-DEBUG.zip" "build\Create_Synchronicity_Setup-r%REV%.exe" "build\latest.txt" "createsoftware,synchronicity@frs.sourceforge.net:/home/pfs/project/s/sy/synchronicity/Create Synchronicity/Unreleased (SVN Builds)"
) >> %LOG%
@goto end

:help
@echo This script is designed to be called by a SVN hook script.
@echo If used from the command line, it should be passed the revision number as its first parameter.
@echo Requires 7-zip installed in "C:\Program Files\7-Zip\7z.exe" and Putty installed in "C:\Program Files (x86)\PuTTY\pscp.exe".
@echo Pageant (putty key handler) should be told about the private key to connect to the scp server.
:end
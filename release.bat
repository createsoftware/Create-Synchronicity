@if "%1" == "/?" goto help

:start
@echo This file is part of Create Synchronicity.
@echo 
@echo Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
@echo Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
@echo You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
@echo Created by:	Clément Pit--Claudel.
@echo Web site:		http://synchronicity.sourceforge.net.

@set VER=%1
@set LOG="build\buildlog-v%VER%.txt"
mkdir build

(echo Packaging log for v%VER% & date /t & time /t & echo.) > %LOG%

"C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE\devenv.exe" "Create Synchronicity.sln" /Rebuild Release /Out %LOG%

(
echo.
echo -----
"C:\Program Files (x86)\NSIS\makensis.exe" "Create Synchronicity\setup_script.nsi"

echo.
echo -----
move Create_Synchronicity_Setup.exe "build\Create_Synchronicity-%VER%_Setup.exe"

cd "Create Synchronicity\bin\Release"
echo.
echo -----
"C:\Program Files\7-Zip\7z.exe" a "..\..\..\build\Create_Synchronicity-%VER%.zip" "Create Synchronicity.exe" "COPYING" "Release notes.txt" "languages\*"
cd ..\..\..
) >> %LOG%
@goto end

:help
@echo This script is designed to be called when packaging for a release.
@echo Usage: release.bat vernum

:end
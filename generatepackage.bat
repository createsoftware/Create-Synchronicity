REM This file is part of Create Synchronicity.
REM 
REM Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
REM Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
REM You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
REM Created by:	Clément Pit--Claudel.
REM Web site:		http://synchronicity.sourceforge.net.

@set REV=%4
mkdir build

(
echo Packaging log for r%REV% & date /t & time /t

echo.
echo -----
"C:\Program Files\NSIS\makensis.exe" "Create Synchronicity\setup_script.nsi"

echo.
echo -----
move Create_Synchronicity_Setup.exe "build\Create_Synchronicity_Setup-r%REV%.exe"

cd "Create Synchronicity\bin\Release"
echo.
echo -----
"C:\Program Files\7-Zip\7z.exe" a "..\..\..\build\Create_Synchronicity-r%REV%.zip" "Release notes.txt" "Create Synchronicity.exe" "COPYING" "languages\*"
cd ..\..\..
) > "build\buildlog-r%REV%.txt"
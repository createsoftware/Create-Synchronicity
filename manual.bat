@if "%1" == "/?" goto help

:start
@echo This file is part of Create Synchronicity.
@echo Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
@echo Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
@echo You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
@echo Created by:   Clément Pit--Claudel.
@echo Web site:     http://synchronicity.sourceforge.net.

Xhtml2Latex.exe "..\..\..\..\..\Sites Web\Sourceforge\Synchronicity\pages\help.php" "build\Create Synchronicity User Manual.tex" "http://synchronicity.sourceforge.net/" "..\..\..\..\..\Sites Web\Sourceforge\Synchronicity\\" /addroot
@goto end

:help
@echo This script will build the PDF Manual for Create Synchronicity.
@echo Usage: manual.bat

:end
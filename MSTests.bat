

rd /S /Q TestResults
md TestResults\HTMLJSReport
 
REM 'Run the .NET Unit Tests with Coverage.'

"%MSTEST_PATH%\MSTest.exe" /nologo /testcontainer:"%UNITTESTPROJECT%\bin\%BUILDMODE%\Tests.Functional.dll" /resultsfile:TestResults\UnitTestReport.trx"

REM 'Convert MSTest report to JUnit report'
"C:\Tools\MSTestToJUnit\msxsl.exe" TestResults\UnitTestReport.trx C:\Tools\MSTestToJUnit\mstest-to-junit.xsl -o TestResults\UnitTestsJUnitReport.xml

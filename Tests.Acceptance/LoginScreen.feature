Feature: Login Screen
	Customer should login
	If provide correct username and password

Scenario: Logging in with invalid credentials
  Given I am at the Login page
  When I fill in the following form
  | field | value |
  | Username | admin |
  | Password | admin |
  And I click the Login button
  Then an error should appear showing 'Can't login! Wrong username or password.'

Scenario: Logging in with valid credentials
  Given I am at the Login page
  When I fill in the following form
  | field | value |
  | Username | admin |
  | Password | Admin1234 |
  And I click the Login button
  Then I should be at the Home page
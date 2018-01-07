Feature: Login
	Test the login functionnality

Scenario: Login Success
	Given I enter 'myusername' in 'Username' textbox
	And I enter 'mypassword' in 'Password' textbox
	When I click 'login' button
	Then I see my profile page

Scenario: Login Invalid Password
	Given I enter 'myusername' in 'Username' textbox
	When I click 'login' button
	Then I see invalid password message
Feature: Register
	Test the Register functionnality

Scenario: Register Success
	Given I enter 'myusername' in 'username' textbox
	And I enter 'myemailaddress' in 'EmailAddress' textbox
	When I click 'Register' button
	Then I see register success message
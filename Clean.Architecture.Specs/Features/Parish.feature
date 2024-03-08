Feature: Parish

A short summary of the feature

@tag1
Scenario: Create a new parish
	Given We have 'Christha Prabhalaya' as parishName, 'Jayanagar 4th block, Bangalore' as parishAddress
	And We have 'Fr. Naveen Kumar' as priestName, '1980-02-12' as priestDoB, 'Austin Town' as priestAddress, '+91 1234567890' as priestPhone
	When We Save these details
	Then We should recieve a parishId

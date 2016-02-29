Payment Service

1. Details of applications, frameworks etc. required to run your solution : 
	This solution is developed using ASP.Net WCF Service (.Net Framework 4.5.2) and Unit tested with the use of MSUnit Test
	
2. Technical description :
	To run this solution, one should unzip the file and open solution in visual studio 2015.
	As this solution contains only the service and fullfledge client is not written to consume this service, one should use PaymentServiceTest project to test the operation contracts implemented.
	However PaymentServiceAgent is created to host the service but not all the contracts are used.
	Unity framework is used to solve DI 
	ClientTest is created using Moq object to test the service call	

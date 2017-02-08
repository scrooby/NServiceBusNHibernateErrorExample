Feature: TestSaga

Scenario: Start saga
Given I have sent a start test saga command
Then the test saga data state should be LongRunningTaskFailed

Scenario: Start saga from failure event
Given I have an existing test saga
When I have sent a long running task failure event
Then the test saga data state should be LongRunningTaskFailed
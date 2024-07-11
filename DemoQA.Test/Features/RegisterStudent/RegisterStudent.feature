Feature: Register Student
    User want to register a new student

    # Background:
    @RegisterStudent @Success
    Scenario Outline: Register with valid all fields data successfully
        Given User opens the DemoQA register Student form page
        When User enters valid data into all fields
            | Field          | Value                   |
            | FirstName      | FNUser                  |
            | LastName       | LNUser                  |
            | Email          | testuser@example.com    |
            | Gender         | Male                    |
            | Mobile         | 0999444666              |
            | DateOfBirth    | 8-October-2002          |
            | Subjects       | Maths, Computer Science |
            | Hobbies        | Sports, Music           |
            | Picture        | TestData\Image\bmo.jpg  |
            | CurrentAddress | 123 quan cam            |
            | State          | Haryana                 |
            | City           | Panipat                 |
        And User clicks on Submit button
        Then Student data shown on pop up should be correct
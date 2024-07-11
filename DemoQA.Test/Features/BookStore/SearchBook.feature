Feature: Search book
    User want to search for books

    # Background:
    @SearchBook @Success
    Scenario Outline: Search book by criteria successfully
        Given User navigates to the book store page
        When User inputs search criteria "<criteria>"
        Then All books match with input criteria will be displayed

        Examples:
            | criteria |
            | design   |
            | Design   |
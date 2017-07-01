
@web
Feature: BudgetCreate

@CleanBudgets
Scenario: Add a budget
Given go to adding budget page
When I add a buget 2000 for "2017-10"
Then it should display "added successfully"

@CleanBudgets
Scenario: Add a budget, but got duplicate successfully
Given go to adding budget page
And Budget table existed budgets
        | Amount | YearMonth |
        | 999    | 2017-10   |
When I add a buget 2000 for "2017-10"
Then it should display "updated successfully"
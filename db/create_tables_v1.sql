
CREATE TABLE user_balance (
    id_user_balance UUID PRIMARY KEY,
    value_balance DECIMAL NOT NULL
);      

CREATE TABLE revenue (
    id_revenue UUID PRIMARY KEY,
    value_revenue DECIMAL NOT NULL,
    date_revenue TIMESTAMP NOT NULL,
    description_revenue VARCHAR(255) NOT NULL,
    category_revenue VARCHAR(45) NOT NULL
    /*UserId VARCHAR(45) NOT NULL,    
    FOREIGN KEY (id_account) REFERENCES AspNetUsers(UserId)*/
);

CREATE TABLE expense (
    id_expense UUID PRIMARY KEY,
    value_expense DECIMAL NOT NULL,
    date_expense TIMESTAMP NOT NULL,
    description_expense VARCHAR(255) NOT NULL
    /*UserId VARCHAR(45) NOT NULL,    
    FOREIGN KEY (id_account) REFERENCES AspNetUsers(UserId)*/
);

CREATE TABLE financial_target (
    id_financial_target UUID PRIMARY KEY,
    title_target VARCHAR(45) NOT NULL,
    value_needed DECIMAL NOT NULL,
    date_limit TIMESTAMP NOT NULL,
    status_target VARCHAR(45) NOT NULL,
    description_target VARCHAR(255) NOT NULL
    /*UserId VARCHAR(45) NOT NULL,    
    FOREIGN KEY (id_account) REFERENCES AspNetUsers(UserId)*/
);

CREATE INDEX idx_user_balance ON user_balance (id_user_balance);
CREATE INDEX idx_revenue ON revenue (id_revenue);
CREATE INDEX idx_expense ON expense (id_expense);
CREATE INDEX idx_financial_target ON financial_target (id_financial_target);
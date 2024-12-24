
CREATE TABLE user_balance (
    id_user_balance VARCHAR(45) PRIMARY KEY,
    value_balance DECIMAL NOT NULL
);      

CREATE TABLE revenue (
    id_revenue VARCHAR(45) PRIMARY KEY,
    value_revenue DECIMAL NOT NULL,
    date_revenue TIMESTAMP NOT NULL,
    description_revenue VARCHAR(255) NOT NULL,
    category_revenue VARCHAR(45) NOT NULL
    /*UserId VARCHAR(45) NOT NULL,    
    FOREIGN KEY (id_account) REFERENCES AspNetUsers(UserId)*/
);

CREATE TABLE expenses (
    id_expenses VARCHAR(45) PRIMARY KEY,
    value_expenses DECIMAL NOT NULL,
    date_expenses TIMESTAMP NOT NULL,
    description_expenses VARCHAR(255) NOT NULL
    /*UserId VARCHAR(45) NOT NULL,    
    FOREIGN KEY (id_account) REFERENCES AspNetUsers(UserId)*/
);

CREATE TABLE objective (
    id_objective VARCHAR(45) PRIMARY KEY,
    title_objective VARCHAR(45) NOT NULL,
    value_needed DECIMAL NOT NULL,
    date_limit TIMESTAMP NOT NULL,
    status_objective VARCHAR(45) NOT NULL,
    description_objective VARCHAR(255) NOT NULL
    /*UserId VARCHAR(45) NOT NULL,    
    FOREIGN KEY (id_account) REFERENCES AspNetUsers(UserId)*/
);
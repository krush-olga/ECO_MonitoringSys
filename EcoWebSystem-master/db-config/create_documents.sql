CREATE TABLE documents
(
    `id`         VARCHAR(20) PRIMARY KEY NOT NULL,
    `name`       TEXT UNIQUE             NOT NULL,
    `body`       TEXT                    NOT NULL,
    `created_on` DATE                    NOT NULL
)

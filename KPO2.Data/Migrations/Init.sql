CREATE TABLE "rss_item"
(
  "id" BIGSERIAL PRIMARY KEY,
  "source" INTEGER DEFAULT 0 NOT NULL,
  "title" TEXT DEFAULT '' NOT NULL,
  "link" TEXT DEFAULT '' NOT NULL,
  "date" TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW() NOT NULL
);


INSERT INTO "rss_item"("source", "title", "link", "date") 
VALUES (@0, @1, @2, @3);
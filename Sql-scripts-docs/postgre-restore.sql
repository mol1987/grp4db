--
-- PostgreSQL database dump
--

-- Dumped from database version 12.1 (Debian 12.1-1.pgdg100+1)
-- Dumped by pg_dump version 12.1 (Debian 12.1-1.pgdg100+1)

-- Started on 2020-02-05 07:49:09 CST

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 230 (class 1255 OID 39904)
-- Name: GetAllEmployees(); Type: FUNCTION; Schema: public; Owner: grupp4
--

CREATE FUNCTION "public"."GetAllEmployees"() RETURNS TABLE("id" integer, "name" character varying, "lastname" character varying, "email" character varying, "password" character varying)
    LANGUAGE "plpgsql"
    AS $$BEGIN
 RETURN QUERY SELECT *FROM "Employees";
END;$$;


ALTER FUNCTION "public"."GetAllEmployees"() OWNER TO "grupp4";

--
-- TOC entry 217 (class 1255 OID 39902)
-- Name: GetAllOrders(); Type: FUNCTION; Schema: public; Owner: grupp4
--

CREATE FUNCTION "public"."GetAllOrders"() RETURNS TABLE("id" integer, "timecreated" "date", "orderstatus" smallint, "price" real, "customerid" integer)
    LANGUAGE "plpgsql"
    AS $$BEGIN
 RETURN QUERY SELECT * FROM "Orders";
END;$$;


ALTER FUNCTION "public"."GetAllOrders"() OWNER TO "grupp4";

--
-- TOC entry 215 (class 1255 OID 39908)
-- Name: getArticleOrders(); Type: FUNCTION; Schema: public; Owner: grupp4
--

CREATE FUNCTION "public"."getArticleOrders"() RETURNS TABLE("articlesid" integer, "ordersid" integer, "count" integer, "id" integer)
    LANGUAGE "plpgsql"
    AS $$BEGIN
RETURN QUERY SELECT * FROM "ArticleOrders";
END;
$$;


ALTER FUNCTION "public"."getArticleOrders"() OWNER TO "grupp4";

--
-- TOC entry 216 (class 1255 OID 39851)
-- Name: getArticles(); Type: FUNCTION; Schema: public; Owner: grupp4
--

CREATE FUNCTION "public"."getArticles"() RETURNS TABLE("id" integer, "name" character varying, "baseprice" integer, "type" character varying)
    LANGUAGE "plpgsql"
    AS $$BEGIN
 RETURN QUERY SELECT * FROM "Articles";
END;$$;


ALTER FUNCTION "public"."getArticles"() OWNER TO "grupp4";

--
-- TOC entry 214 (class 1255 OID 39906)
-- Name: getIngredients(); Type: FUNCTION; Schema: public; Owner: grupp4
--

CREATE FUNCTION "public"."getIngredients"() RETURNS TABLE("id" integer, "name" character varying, "price" real)
    LANGUAGE "plpgsql"
    AS $$BEGIN
 RETURN QUERY SELECT * FROM "Ingredients";
END;$$;


ALTER FUNCTION "public"."getIngredients"() OWNER TO "grupp4";

SET default_tablespace = '';

SET default_table_access_method = "heap";

--
-- TOC entry 203 (class 1259 OID 39749)
-- Name: ArticleIngredients; Type: TABLE; Schema: public; Owner: grupp4
--

CREATE TABLE "public"."ArticleIngredients" (
    "ArticleID" integer NOT NULL,
    "IngredientID" integer NOT NULL
);


ALTER TABLE "public"."ArticleIngredients" OWNER TO "grupp4";

--
-- TOC entry 202 (class 1259 OID 39744)
-- Name: ArticleOrders; Type: TABLE; Schema: public; Owner: grupp4
--

CREATE TABLE "public"."ArticleOrders" (
    "ArticlesID" integer,
    "OrdersID" integer,
    "Count" integer,
    "ID" integer NOT NULL
);


ALTER TABLE "public"."ArticleOrders" OWNER TO "grupp4";

--
-- TOC entry 205 (class 1259 OID 39756)
-- Name: ArticleOrdersIngredients; Type: TABLE; Schema: public; Owner: grupp4
--

CREATE TABLE "public"."ArticleOrdersIngredients" (
    "ID" integer NOT NULL,
    "IngredientsID" integer,
    "ArticleOrdersID" integer
);


ALTER TABLE "public"."ArticleOrdersIngredients" OWNER TO "grupp4";

--
-- TOC entry 204 (class 1259 OID 39754)
-- Name: ArticleOrdersIngredients_ID_seq; Type: SEQUENCE; Schema: public; Owner: grupp4
--

CREATE SEQUENCE "public"."ArticleOrdersIngredients_ID_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE "public"."ArticleOrdersIngredients_ID_seq" OWNER TO "grupp4";

--
-- TOC entry 2982 (class 0 OID 0)
-- Dependencies: 204
-- Name: ArticleOrdersIngredients_ID_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: grupp4
--

ALTER SEQUENCE "public"."ArticleOrdersIngredients_ID_seq" OWNED BY "public"."ArticleOrdersIngredients"."ID";


--
-- TOC entry 213 (class 1259 OID 39805)
-- Name: Articles; Type: TABLE; Schema: public; Owner: grupp4
--

CREATE TABLE "public"."Articles" (
    "ID" integer NOT NULL,
    "Name" character varying(50),
    "BasePrice" integer,
    "Type" character varying(50)
);


ALTER TABLE "public"."Articles" OWNER TO "grupp4";

--
-- TOC entry 212 (class 1259 OID 39803)
-- Name: Articles_ID_seq; Type: SEQUENCE; Schema: public; Owner: grupp4
--

CREATE SEQUENCE "public"."Articles_ID_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE "public"."Articles_ID_seq" OWNER TO "grupp4";

--
-- TOC entry 2983 (class 0 OID 0)
-- Dependencies: 212
-- Name: Articles_ID_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: grupp4
--

ALTER SEQUENCE "public"."Articles_ID_seq" OWNED BY "public"."Articles"."ID";


--
-- TOC entry 207 (class 1259 OID 39775)
-- Name: Employees; Type: TABLE; Schema: public; Owner: grupp4
--

CREATE TABLE "public"."Employees" (
    "ID" integer NOT NULL,
    "Name" character varying(50),
    "LastName" character varying(50),
    "Email" character varying(50) NOT NULL,
    "Password" character varying(50) NOT NULL
);


ALTER TABLE "public"."Employees" OWNER TO "grupp4";

--
-- TOC entry 206 (class 1259 OID 39773)
-- Name: Employees_ID_seq; Type: SEQUENCE; Schema: public; Owner: grupp4
--

CREATE SEQUENCE "public"."Employees_ID_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE "public"."Employees_ID_seq" OWNER TO "grupp4";

--
-- TOC entry 2984 (class 0 OID 0)
-- Dependencies: 206
-- Name: Employees_ID_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: grupp4
--

ALTER SEQUENCE "public"."Employees_ID_seq" OWNED BY "public"."Employees"."ID";


--
-- TOC entry 209 (class 1259 OID 39786)
-- Name: Ingredients; Type: TABLE; Schema: public; Owner: grupp4
--

CREATE TABLE "public"."Ingredients" (
    "ID" integer NOT NULL,
    "Name" character varying(50),
    "Price" real
);


ALTER TABLE "public"."Ingredients" OWNER TO "grupp4";

--
-- TOC entry 208 (class 1259 OID 39784)
-- Name: Ingredients_ID_seq; Type: SEQUENCE; Schema: public; Owner: grupp4
--

CREATE SEQUENCE "public"."Ingredients_ID_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE "public"."Ingredients_ID_seq" OWNER TO "grupp4";

--
-- TOC entry 2985 (class 0 OID 0)
-- Dependencies: 208
-- Name: Ingredients_ID_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: grupp4
--

ALTER SEQUENCE "public"."Ingredients_ID_seq" OWNED BY "public"."Ingredients"."ID";


--
-- TOC entry 211 (class 1259 OID 39797)
-- Name: Orders; Type: TABLE; Schema: public; Owner: grupp4
--

CREATE TABLE "public"."Orders" (
    "ID" integer NOT NULL,
    "TimeCreated" "date",
    "Orderstatus" smallint,
    "Price" real,
    "CustomerID" integer
);


ALTER TABLE "public"."Orders" OWNER TO "grupp4";

--
-- TOC entry 210 (class 1259 OID 39795)
-- Name: Orders_ID_seq; Type: SEQUENCE; Schema: public; Owner: grupp4
--

CREATE SEQUENCE "public"."Orders_ID_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE "public"."Orders_ID_seq" OWNER TO "grupp4";

--
-- TOC entry 2986 (class 0 OID 0)
-- Dependencies: 210
-- Name: Orders_ID_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: grupp4
--

ALTER SEQUENCE "public"."Orders_ID_seq" OWNED BY "public"."Orders"."ID";


--
-- TOC entry 2814 (class 2604 OID 39759)
-- Name: ArticleOrdersIngredients ID; Type: DEFAULT; Schema: public; Owner: grupp4
--

ALTER TABLE ONLY "public"."ArticleOrdersIngredients" ALTER COLUMN "ID" SET DEFAULT "nextval"('"public"."ArticleOrdersIngredients_ID_seq"'::"regclass");


--
-- TOC entry 2818 (class 2604 OID 39808)
-- Name: Articles ID; Type: DEFAULT; Schema: public; Owner: grupp4
--

ALTER TABLE ONLY "public"."Articles" ALTER COLUMN "ID" SET DEFAULT "nextval"('"public"."Articles_ID_seq"'::"regclass");


--
-- TOC entry 2815 (class 2604 OID 39778)
-- Name: Employees ID; Type: DEFAULT; Schema: public; Owner: grupp4
--

ALTER TABLE ONLY "public"."Employees" ALTER COLUMN "ID" SET DEFAULT "nextval"('"public"."Employees_ID_seq"'::"regclass");


--
-- TOC entry 2816 (class 2604 OID 39789)
-- Name: Ingredients ID; Type: DEFAULT; Schema: public; Owner: grupp4
--

ALTER TABLE ONLY "public"."Ingredients" ALTER COLUMN "ID" SET DEFAULT "nextval"('"public"."Ingredients_ID_seq"'::"regclass");


--
-- TOC entry 2817 (class 2604 OID 39800)
-- Name: Orders ID; Type: DEFAULT; Schema: public; Owner: grupp4
--

ALTER TABLE ONLY "public"."Orders" ALTER COLUMN "ID" SET DEFAULT "nextval"('"public"."Orders_ID_seq"'::"regclass");


--
-- TOC entry 2966 (class 0 OID 39749)
-- Dependencies: 203
-- Data for Name: ArticleIngredients; Type: TABLE DATA; Schema: public; Owner: grupp4
--

INSERT INTO "public"."ArticleIngredients" ("ArticleID", "IngredientID") VALUES (2, 2);
INSERT INTO "public"."ArticleIngredients" ("ArticleID", "IngredientID") VALUES (3, 1);
INSERT INTO "public"."ArticleIngredients" ("ArticleID", "IngredientID") VALUES (3, 2);
INSERT INTO "public"."ArticleIngredients" ("ArticleID", "IngredientID") VALUES (3, 3);
INSERT INTO "public"."ArticleIngredients" ("ArticleID", "IngredientID") VALUES (3, 5);
INSERT INTO "public"."ArticleIngredients" ("ArticleID", "IngredientID") VALUES (4, 1);
INSERT INTO "public"."ArticleIngredients" ("ArticleID", "IngredientID") VALUES (4, 2);
INSERT INTO "public"."ArticleIngredients" ("ArticleID", "IngredientID") VALUES (4, 3);
INSERT INTO "public"."ArticleIngredients" ("ArticleID", "IngredientID") VALUES (5, 2);
INSERT INTO "public"."ArticleIngredients" ("ArticleID", "IngredientID") VALUES (5, 3);
INSERT INTO "public"."ArticleIngredients" ("ArticleID", "IngredientID") VALUES (5, 9);
INSERT INTO "public"."ArticleIngredients" ("ArticleID", "IngredientID") VALUES (6, 1);
INSERT INTO "public"."ArticleIngredients" ("ArticleID", "IngredientID") VALUES (6, 2);
INSERT INTO "public"."ArticleIngredients" ("ArticleID", "IngredientID") VALUES (6, 6);


--
-- TOC entry 2965 (class 0 OID 39744)
-- Dependencies: 202
-- Data for Name: ArticleOrders; Type: TABLE DATA; Schema: public; Owner: grupp4
--

INSERT INTO "public"."ArticleOrders" ("ArticlesID", "OrdersID", "Count", "ID") VALUES (4, 2, NULL, 6);
INSERT INTO "public"."ArticleOrders" ("ArticlesID", "OrdersID", "Count", "ID") VALUES (6, 2, NULL, 7);
INSERT INTO "public"."ArticleOrders" ("ArticlesID", "OrdersID", "Count", "ID") VALUES (1, 2, NULL, 15);
INSERT INTO "public"."ArticleOrders" ("ArticlesID", "OrdersID", "Count", "ID") VALUES (1, 2, NULL, 16);
INSERT INTO "public"."ArticleOrders" ("ArticlesID", "OrdersID", "Count", "ID") VALUES (1, 2, NULL, 17);
INSERT INTO "public"."ArticleOrders" ("ArticlesID", "OrdersID", "Count", "ID") VALUES (3, 2, NULL, 5);
INSERT INTO "public"."ArticleOrders" ("ArticlesID", "OrdersID", "Count", "ID") VALUES (1, 2, NULL, 1);
INSERT INTO "public"."ArticleOrders" ("ArticlesID", "OrdersID", "Count", "ID") VALUES (2, 2, NULL, 2);
INSERT INTO "public"."ArticleOrders" ("ArticlesID", "OrdersID", "Count", "ID") VALUES (2, 3, NULL, 3);
INSERT INTO "public"."ArticleOrders" ("ArticlesID", "OrdersID", "Count", "ID") VALUES (2, 4, NULL, 4);


--
-- TOC entry 2968 (class 0 OID 39756)
-- Dependencies: 205
-- Data for Name: ArticleOrdersIngredients; Type: TABLE DATA; Schema: public; Owner: grupp4
--

INSERT INTO "public"."ArticleOrdersIngredients" ("ID", "IngredientsID", "ArticleOrdersID") VALUES (1, 1, 17);
INSERT INTO "public"."ArticleOrdersIngredients" ("ID", "IngredientsID", "ArticleOrdersID") VALUES (2, 2, 17);
INSERT INTO "public"."ArticleOrdersIngredients" ("ID", "IngredientsID", "ArticleOrdersID") VALUES (3, 3, 17);
INSERT INTO "public"."ArticleOrdersIngredients" ("ID", "IngredientsID", "ArticleOrdersID") VALUES (4, 1, 18);
INSERT INTO "public"."ArticleOrdersIngredients" ("ID", "IngredientsID", "ArticleOrdersID") VALUES (5, 2, 18);
INSERT INTO "public"."ArticleOrdersIngredients" ("ID", "IngredientsID", "ArticleOrdersID") VALUES (6, 1, 21);
INSERT INTO "public"."ArticleOrdersIngredients" ("ID", "IngredientsID", "ArticleOrdersID") VALUES (7, 2, 21);
INSERT INTO "public"."ArticleOrdersIngredients" ("ID", "IngredientsID", "ArticleOrdersID") VALUES (8, 3, 21);
INSERT INTO "public"."ArticleOrdersIngredients" ("ID", "IngredientsID", "ArticleOrdersID") VALUES (9, 1, 22);
INSERT INTO "public"."ArticleOrdersIngredients" ("ID", "IngredientsID", "ArticleOrdersID") VALUES (10, 2, 22);


--
-- TOC entry 2976 (class 0 OID 39805)
-- Dependencies: 213
-- Data for Name: Articles; Type: TABLE DATA; Schema: public; Owner: grupp4
--

INSERT INTO "public"."Articles" ("ID", "Name", "BasePrice", "Type") VALUES (1, 'Vesuvio', 99, 'Pizza');
INSERT INTO "public"."Articles" ("ID", "Name", "BasePrice", "Type") VALUES (2, 'Margaita', 85, 'Pizza');
INSERT INTO "public"."Articles" ("ID", "Name", "BasePrice", "Type") VALUES (3, 'Hawaii', 85, 'Pizza');
INSERT INTO "public"."Articles" ("ID", "Name", "BasePrice", "Type") VALUES (4, 'Calzone', 85, 'Pizza');
INSERT INTO "public"."Articles" ("ID", "Name", "BasePrice", "Type") VALUES (5, 'Napoli', 90, 'Pizza');
INSERT INTO "public"."Articles" ("ID", "Name", "BasePrice", "Type") VALUES (6, 'Kebabpizza', 90, 'Pizza');
INSERT INTO "public"."Articles" ("ID", "Name", "BasePrice", "Type") VALUES (7, 'Inadiana', 95, 'Pizza');
INSERT INTO "public"."Articles" ("ID", "Name", "BasePrice", "Type") VALUES (8, 'Cariccio', 85, 'Pizza');
INSERT INTO "public"."Articles" ("ID", "Name", "BasePrice", "Type") VALUES (9, 'Potatissallad', 80, 'Sallad');
INSERT INTO "public"."Articles" ("ID", "Name", "BasePrice", "Type") VALUES (10, 'potatissallad', 80, 'Sallad');
INSERT INTO "public"."Articles" ("ID", "Name", "BasePrice", "Type") VALUES (11, 'Potatissallad', 80, 'Sallad');
INSERT INTO "public"."Articles" ("ID", "Name", "BasePrice", "Type") VALUES (12, 'Potatissallad', 80, 'Sallad');
INSERT INTO "public"."Articles" ("ID", "Name", "BasePrice", "Type") VALUES (13, 'Potatissallad', 80, 'Sallad');
INSERT INTO "public"."Articles" ("ID", "Name", "BasePrice", "Type") VALUES (14, 'Potatissalad', 80, 'Sallad');


--
-- TOC entry 2970 (class 0 OID 39775)
-- Dependencies: 207
-- Data for Name: Employees; Type: TABLE DATA; Schema: public; Owner: grupp4
--

INSERT INTO "public"."Employees" ("ID", "Name", "LastName", "Email", "Password") VALUES (3, 'Jacob', 'Andres', 'Jacob@kth.se', '456');
INSERT INTO "public"."Employees" ("ID", "Name", "LastName", "Email", "Password") VALUES (4, 'Firas', 'Hans', 'Haans@kth.se', '478');
INSERT INTO "public"."Employees" ("ID", "Name", "LastName", "Email", "Password") VALUES (5, 'Jesse', 'Petersson', 'Jesse@kth.se', '891');
INSERT INTO "public"."Employees" ("ID", "Name", "LastName", "Email", "Password") VALUES (1, 'Fredrik', 'Molle', 'Molle@kth.se', '123');
INSERT INTO "public"."Employees" ("ID", "Name", "LastName", "Email", "Password") VALUES (2, 'Meles', 'Bitow', 'Bitow@kth.se', '234');


--
-- TOC entry 2972 (class 0 OID 39786)
-- Dependencies: 209
-- Data for Name: Ingredients; Type: TABLE DATA; Schema: public; Owner: grupp4
--

INSERT INTO "public"."Ingredients" ("ID", "Name", "Price") VALUES (1, 'Ost', 0);
INSERT INTO "public"."Ingredients" ("ID", "Name", "Price") VALUES (2, 'Tomatås', 0);
INSERT INTO "public"."Ingredients" ("ID", "Name", "Price") VALUES (3, 'Skinka', 0);
INSERT INTO "public"."Ingredients" ("ID", "Name", "Price") VALUES (4, 'Champinjoner', 0);
INSERT INTO "public"."Ingredients" ("ID", "Name", "Price") VALUES (5, 'Ananas', 0);
INSERT INTO "public"."Ingredients" ("ID", "Name", "Price") VALUES (6, 'Kebabkött', 0);
INSERT INTO "public"."Ingredients" ("ID", "Name", "Price") VALUES (7, 'Svamp', 0);
INSERT INTO "public"."Ingredients" ("ID", "Name", "Price") VALUES (8, 'Spenat', 0);
INSERT INTO "public"."Ingredients" ("ID", "Name", "Price") VALUES (9, 'Mozzarella', 0);
INSERT INTO "public"."Ingredients" ("ID", "Name", "Price") VALUES (10, 'Extra_Ost', 0);
INSERT INTO "public"."Ingredients" ("ID", "Name", "Price") VALUES (11, 'Size_XL', 40);


--
-- TOC entry 2974 (class 0 OID 39797)
-- Dependencies: 211
-- Data for Name: Orders; Type: TABLE DATA; Schema: public; Owner: grupp4
--

INSERT INTO "public"."Orders" ("ID", "TimeCreated", "Orderstatus", "Price", "CustomerID") VALUES (2, '2020-01-24', 2, 0, 999);
INSERT INTO "public"."Orders" ("ID", "TimeCreated", "Orderstatus", "Price", "CustomerID") VALUES (3, '2020-01-24', 2, 0, 999);
INSERT INTO "public"."Orders" ("ID", "TimeCreated", "Orderstatus", "Price", "CustomerID") VALUES (4, '20202-01-24', 2, 0, 999);
INSERT INTO "public"."Orders" ("ID", "TimeCreated", "Orderstatus", "Price", "CustomerID") VALUES (5, '2020-01-24', 2, 0, 999);
INSERT INTO "public"."Orders" ("ID", "TimeCreated", "Orderstatus", "Price", "CustomerID") VALUES (6, '2020-01-24', 2, 0, 999);
INSERT INTO "public"."Orders" ("ID", "TimeCreated", "Orderstatus", "Price", "CustomerID") VALUES (8, '2020-01-24', 3, 99, 99);
INSERT INTO "public"."Orders" ("ID", "TimeCreated", "Orderstatus", "Price", "CustomerID") VALUES (9, '2020-01-24', 1, 99, 1);
INSERT INTO "public"."Orders" ("ID", "TimeCreated", "Orderstatus", "Price", "CustomerID") VALUES (10, '20202-01-24', 0, 10, 1);
INSERT INTO "public"."Orders" ("ID", "TimeCreated", "Orderstatus", "Price", "CustomerID") VALUES (11, '2020-01-24', 0, 10, 1);
INSERT INTO "public"."Orders" ("ID", "TimeCreated", "Orderstatus", "Price", "CustomerID") VALUES (12, '2020-01-24', 0, 124, 1);


--
-- TOC entry 2987 (class 0 OID 0)
-- Dependencies: 204
-- Name: ArticleOrdersIngredients_ID_seq; Type: SEQUENCE SET; Schema: public; Owner: grupp4
--

SELECT pg_catalog.setval('"public"."ArticleOrdersIngredients_ID_seq"', 1, false);


--
-- TOC entry 2988 (class 0 OID 0)
-- Dependencies: 212
-- Name: Articles_ID_seq; Type: SEQUENCE SET; Schema: public; Owner: grupp4
--

SELECT pg_catalog.setval('"public"."Articles_ID_seq"', 1, false);


--
-- TOC entry 2989 (class 0 OID 0)
-- Dependencies: 206
-- Name: Employees_ID_seq; Type: SEQUENCE SET; Schema: public; Owner: grupp4
--

SELECT pg_catalog.setval('"public"."Employees_ID_seq"', 1, false);


--
-- TOC entry 2990 (class 0 OID 0)
-- Dependencies: 208
-- Name: Ingredients_ID_seq; Type: SEQUENCE SET; Schema: public; Owner: grupp4
--

SELECT pg_catalog.setval('"public"."Ingredients_ID_seq"', 1, false);


--
-- TOC entry 2991 (class 0 OID 0)
-- Dependencies: 210
-- Name: Orders_ID_seq; Type: SEQUENCE SET; Schema: public; Owner: grupp4
--

SELECT pg_catalog.setval('"public"."Orders_ID_seq"', 1, false);


--
-- TOC entry 2824 (class 2606 OID 39753)
-- Name: ArticleIngredients ArticleIngredients_pkey; Type: CONSTRAINT; Schema: public; Owner: grupp4
--

ALTER TABLE ONLY "public"."ArticleIngredients"
    ADD CONSTRAINT "ArticleIngredients_pkey" PRIMARY KEY ("ArticleID", "IngredientID");


--
-- TOC entry 2827 (class 2606 OID 39761)
-- Name: ArticleOrdersIngredients ArticleOrdersIngredients_pkey; Type: CONSTRAINT; Schema: public; Owner: grupp4
--

ALTER TABLE ONLY "public"."ArticleOrdersIngredients"
    ADD CONSTRAINT "ArticleOrdersIngredients_pkey" PRIMARY KEY ("ID");


--
-- TOC entry 2820 (class 2606 OID 39748)
-- Name: ArticleOrders ArticleOrders_pkey; Type: CONSTRAINT; Schema: public; Owner: grupp4
--

ALTER TABLE ONLY "public"."ArticleOrders"
    ADD CONSTRAINT "ArticleOrders_pkey" PRIMARY KEY ("ID");


--
-- TOC entry 2835 (class 2606 OID 39813)
-- Name: Articles Articles_pkey; Type: CONSTRAINT; Schema: public; Owner: grupp4
--

ALTER TABLE ONLY "public"."Articles"
    ADD CONSTRAINT "Articles_pkey" PRIMARY KEY ("ID");


--
-- TOC entry 2829 (class 2606 OID 39783)
-- Name: Employees Employees_pkey; Type: CONSTRAINT; Schema: public; Owner: grupp4
--

ALTER TABLE ONLY "public"."Employees"
    ADD CONSTRAINT "Employees_pkey" PRIMARY KEY ("ID");


--
-- TOC entry 2831 (class 2606 OID 39794)
-- Name: Ingredients Ingredients_pkey; Type: CONSTRAINT; Schema: public; Owner: grupp4
--

ALTER TABLE ONLY "public"."Ingredients"
    ADD CONSTRAINT "Ingredients_pkey" PRIMARY KEY ("ID");


--
-- TOC entry 2833 (class 2606 OID 39802)
-- Name: Orders Orders_pkey; Type: CONSTRAINT; Schema: public; Owner: grupp4
--

ALTER TABLE ONLY "public"."Orders"
    ADD CONSTRAINT "Orders_pkey" PRIMARY KEY ("ID");


--
-- TOC entry 2825 (class 1259 OID 39820)
-- Name: fki_ArticleIngredients-FK; Type: INDEX; Schema: public; Owner: grupp4
--

CREATE INDEX "fki_ArticleIngredients-FK" ON "public"."ArticleIngredients" USING "btree" ("ArticleID");


--
-- TOC entry 2821 (class 1259 OID 39826)
-- Name: fki_ArticleOrders_Articles_FK; Type: INDEX; Schema: public; Owner: grupp4
--

CREATE INDEX "fki_ArticleOrders_Articles_FK" ON "public"."ArticleOrders" USING "btree" ("ArticlesID");


--
-- TOC entry 2822 (class 1259 OID 39832)
-- Name: fki_ArticleOrders_Orders_FK; Type: INDEX; Schema: public; Owner: grupp4
--

CREATE INDEX "fki_ArticleOrders_Orders_FK" ON "public"."ArticleOrders" USING "btree" ("OrdersID");


--
-- TOC entry 2838 (class 2606 OID 39815)
-- Name: ArticleIngredients ArticleIngredients-FK; Type: FK CONSTRAINT; Schema: public; Owner: grupp4
--

ALTER TABLE ONLY "public"."ArticleIngredients"
    ADD CONSTRAINT "ArticleIngredients-FK" FOREIGN KEY ("ArticleID") REFERENCES "public"."Articles"("ID") NOT VALID;


--
-- TOC entry 2837 (class 2606 OID 39821)
-- Name: ArticleOrders ArticleOrders_Articles_FK; Type: FK CONSTRAINT; Schema: public; Owner: grupp4
--

ALTER TABLE ONLY "public"."ArticleOrders"
    ADD CONSTRAINT "ArticleOrders_Articles_FK" FOREIGN KEY ("ArticlesID") REFERENCES "public"."Articles"("ID");


--
-- TOC entry 2836 (class 2606 OID 39827)
-- Name: ArticleOrders ArticleOrders_Orders_FK; Type: FK CONSTRAINT; Schema: public; Owner: grupp4
--

ALTER TABLE ONLY "public"."ArticleOrders"
    ADD CONSTRAINT "ArticleOrders_Orders_FK" FOREIGN KEY ("OrdersID") REFERENCES "public"."Orders"("ID") NOT VALID;


-- Completed on 2020-02-05 07:49:09 CST

--
-- PostgreSQL database dump complete
--


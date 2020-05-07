--
-- PostgreSQL database dump
--

-- Dumped from database version 12.1 (Debian 12.1-1.pgdg100+1)
-- Dumped by pg_dump version 12.0

-- Started on 2020-02-13 23:13:29

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
-- TOC entry 2971 (class 0 OID 16803)
-- Dependencies: 206
-- Data for Name: Articles; Type: TABLE DATA; Schema: public; Owner: grupp4
--

COPY public."Articles" ("ID", "Name", "BasePrice", "Type") FROM stdin;
1	Vesuvio	99	Pizza
2	Margaita	85	Pizza
3	Hawaii	85	Pizza
4	Calzone	85	Pizza
5	Napoli	90	Pizza
6	Kebabpizza	90	Pizza
7	Inadiana	95	Pizza
8	Cariccio	85	Pizza
9	Potatissallad	80	Sallad
10	potatissallad	80	Sallad
11	Potatissallad	80	Sallad
12	Potatissallad	80	Sallad
13	Potatissallad	80	Sallad
14	Potatissalad	80	Sallad
\.


--
-- TOC entry 2967 (class 0 OID 16792)
-- Dependencies: 202
-- Data for Name: ArticleIngredients; Type: TABLE DATA; Schema: public; Owner: grupp4
--

COPY public."ArticleIngredients" ("ArticleID", "IngredientID") FROM stdin;
2	2
3	1
3	2
3	3
3	5
4	1
4	2
4	3
5	2
5	3
5	9
6	1
6	2
6	6
\.


--
-- TOC entry 2977 (class 0 OID 16818)
-- Dependencies: 212
-- Data for Name: Orders; Type: TABLE DATA; Schema: public; Owner: grupp4
--

COPY public."Orders" ("ID", "TimeCreated", "Orderstatus", "Price", "CustomerID") FROM stdin;
2	2020-01-24	2	0	999
3	2020-01-24	2	0	999
4	20202-01-24	2	0	999
5	2020-01-24	2	0	999
6	2020-01-24	2	0	999
8	2020-01-24	3	99	99
9	2020-01-24	1	99	1
10	20202-01-24	0	10	1
11	2020-01-24	0	10	1
12	2020-01-24	0	124	1
\.


--
-- TOC entry 2968 (class 0 OID 16795)
-- Dependencies: 203
-- Data for Name: ArticleOrders; Type: TABLE DATA; Schema: public; Owner: grupp4
--

COPY public."ArticleOrders" ("ArticlesID", "OrdersID", "Count", "ID") FROM stdin;
4	2	\N	6
6	2	\N	7
1	2	\N	15
1	2	\N	16
1	2	\N	17
3	2	\N	5
1	2	\N	1
2	2	\N	2
2	3	\N	3
2	4	\N	4
\.


--
-- TOC entry 2969 (class 0 OID 16798)
-- Dependencies: 204
-- Data for Name: ArticleOrdersIngredients; Type: TABLE DATA; Schema: public; Owner: grupp4
--

COPY public."ArticleOrdersIngredients" ("ID", "IngredientsID", "ArticleOrdersID") FROM stdin;
1	1	17
2	2	17
3	3	17
4	1	18
5	2	18
6	1	21
7	2	21
8	3	21
9	1	22
10	2	22
\.


--
-- TOC entry 2973 (class 0 OID 16808)
-- Dependencies: 208
-- Data for Name: Employees; Type: TABLE DATA; Schema: public; Owner: grupp4
--

COPY public."Employees" ("ID", "Name", "LastName", "Email", "Password") FROM stdin;
3	Jacob	Andres	Jacob@kth.se	456
4	Firas	Hans	Haans@kth.se	478
5	Jesse	Petersson	Jesse@kth.se	891
1	Fredrik	Molle	Molle@kth.se	123
2	Meles	Bitow	Bitow@kth.se	234
\.


--
-- TOC entry 2975 (class 0 OID 16813)
-- Dependencies: 210
-- Data for Name: Ingredients; Type: TABLE DATA; Schema: public; Owner: grupp4
--

COPY public."Ingredients" ("ID", "Name", "Price") FROM stdin;
1	Ost	0
2	Tomatås	0
3	Skinka	0
4	Champinjoner	0
5	Ananas	0
6	Kebabkött	0
7	Svamp	0
8	Spenat	0
9	Mozzarella	0
10	Extra_Ost	0
11	Size_XL	40
\.


--
-- TOC entry 2984 (class 0 OID 0)
-- Dependencies: 205
-- Name: ArticleOrdersIngredients_ID_seq; Type: SEQUENCE SET; Schema: public; Owner: grupp4
--

SELECT pg_catalog.setval('public."ArticleOrdersIngredients_ID_seq"', 1, false);


--
-- TOC entry 2985 (class 0 OID 0)
-- Dependencies: 207
-- Name: Articles_ID_seq; Type: SEQUENCE SET; Schema: public; Owner: grupp4
--

SELECT pg_catalog.setval('public."Articles_ID_seq"', 1, false);


--
-- TOC entry 2986 (class 0 OID 0)
-- Dependencies: 209
-- Name: Employees_ID_seq; Type: SEQUENCE SET; Schema: public; Owner: grupp4
--

SELECT pg_catalog.setval('public."Employees_ID_seq"', 1, false);


--
-- TOC entry 2987 (class 0 OID 0)
-- Dependencies: 211
-- Name: Ingredients_ID_seq; Type: SEQUENCE SET; Schema: public; Owner: grupp4
--

SELECT pg_catalog.setval('public."Ingredients_ID_seq"', 1, false);


--
-- TOC entry 2988 (class 0 OID 0)
-- Dependencies: 213
-- Name: Orders_ID_seq; Type: SEQUENCE SET; Schema: public; Owner: grupp4
--

SELECT pg_catalog.setval('public."Orders_ID_seq"', 1, false);


-- Completed on 2020-02-13 23:13:31

--
-- PostgreSQL database dump complete
--


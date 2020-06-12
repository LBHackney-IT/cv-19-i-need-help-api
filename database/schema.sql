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

SET default_tablespace = '';

SET default_with_oids = false;

--
-- Name: delivery_batch id; Type: DEFAULT; Schema: public; Owner: postgres
--

CREATE TABLE public.delivery_batch (
    "id" int4 NOT NULL,
    "delivery_date" date,
    "delivery_packages" int4,
    "report_file_id" varchar,
    "status" varchar,
    PRIMARY KEY ("id")
);

ALTER TABLE public.delivery_batch OWNER TO postgres;

-- Sequence and defined type
CREATE SEQUENCE public.delivery_batch_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

--
-- Name: delivery_batch id; Type: DEFAULT; Schema: public; Owner: postgres
--
    
ALTER TABLE public.delivery_batch_id_seq OWNER TO postgres;

--
-- Name: delivery_batch id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.delivery_batch_id_seq OWNED BY public.delivery_batch.id;

--
-- Name: delivery_batch id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.delivery_batch ALTER COLUMN id SET DEFAULT nextval('public.delivery_batch_id_seq'::regclass);

-----
--
-- Name: delivery_report_data id; Type: DEFAULT; Schema: public; Owner: postgres
--

CREATE TABLE public.delivery_report_data (
    "id" int4 NOT NULL,
    "annex_id" int4,
    "num_of_packages" int4,
    "full_name" varchar,
    "contact_telephone_number" varchar,
    "contact_mobile_number" varchar,
    "full_address" varchar,
    "postcode" varchar,
    "uprn" varchar,
    "any_food_household_cannot_eat" varchar,
    "delivery_notes" text,
    "delivery_date" date,
    "last_confirmed_delivery_date" date,
    "batch_id" int4,
    PRIMARY KEY ("id")
);

ALTER TABLE public.delivery_report_data OWNER TO postgres;

-- Sequence and defined type
CREATE SEQUENCE public.delivery_report_data_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

--
-- Name: delivery_batch id; Type: DEFAULT; Schema: public; Owner: postgres
--
    
ALTER TABLE public.delivery_report_data_id_seq OWNER TO postgres;

--
-- Name: delivery_batch id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.delivery_report_data_id_seq OWNED BY public.delivery_report_data.id;

--
-- Name: delivery_report_data id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.delivery_batch ALTER COLUMN id SET DEFAULT nextval('public.delivery_report_data_id_seq'::regclass);

-----

--
-- Name: resident_support_annex; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.resident_support_annex (
    id integer NOT NULL,
    is_duplicate character varying,
    ongoing_food_need boolean,
    ongoing_prescription_need boolean,
    form_id character varying,
    form_version character varying,
    date_time_recorded timestamp without time zone,
    first_name character varying,
    last_name character varying,
    dob_month character varying,
    dob_year character varying,
    dob_day character varying,
    postcode character varying,
    uprn character varying,
    ward character varying,
    address_first_line character varying,
    address_second_line character varying,
    address_third_line character varying,
    contact_telephone_number character varying,
    contact_mobile_number character varying,
    email_address character varying,
    is_on_behalf boolean,
    on_behalf_first_name character varying,
    on_behalf_last_name character varying,
    on_behalf_email_address character varying,
    on_behalf_contact_number character varying,
    relationship_with_resident character varying,
    anything_else character varying,
    gp_surgery_details character varying,
    food_need boolean,
    number_of_people_in_house character varying,
    days_worth_of_food character varying,
    any_food_household_cannot_eat character varying,
    struggling_to_pay_for_food boolean,
    is_pharmacist_able_to_deliver boolean,
    name_address_pharmacist character varying,
    is_package_of_care_asc boolean,
    is_urgent_food_required boolean,
    days_worth_of_medicine character varying,
    is_urgent_medicine_required boolean,
    is_address_confirmed boolean,
    is_household_help_available boolean,
    is_urgent_food boolean,
    is_urgent_prescription boolean,
    any_help_available boolean,
    is_any_aged_under_15 boolean,
    last_confirmed_food_delivery date,
    record_status character varying,
    case_notes text,
    delivery_notes text
);

ALTER TABLE public.resident_support_annex OWNER TO postgres;

--
-- Name: resident_support_annex_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.resident_support_annex_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER TABLE public.resident_support_annex_id_seq OWNER TO postgres;

--
-- Name: resident_support_annex_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--
ALTER SEQUENCE public.resident_support_annex_id_seq OWNED BY public.resident_support_annex.id;


--
-- Name: resident_support_annex id; Type: DEFAULT; Schema: public; Owner: postgres
--
ALTER TABLE ONLY public.resident_support_annex ALTER COLUMN id SET DEFAULT nextval('public.resident_support_annex_id_seq'::regclass);


--
-- Name: resident_support_annex resident_support_annex_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--
ALTER TABLE ONLY public.resident_support_annex
    ADD CONSTRAINT resident_support_annex_pkey PRIMARY KEY (id);


--
-- Name: resident_support_annex annex_insert_trigger; Type: TRIGGER; Schema: public; Owner: postgres
--
CREATE FUNCTION public.set_duplicate_flag()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function1$
BEGIN
        IF (SELECT count(id)
                FROM resident_support_annex
                WHERE TRIM(uprn) is not distinct from TRIM(NEW.uprn)
                AND TRIM(dob_day) is not distinct from TRIM(NEW.dob_day)
                AND TRIM(dob_month) is not distinct from TRIM(NEW.dob_month)
                AND TRIM(dob_year) is not distinct from TRIM(NEW.dob_year)
                AND (TRIM(contact_telephone_number) is not distinct from TRIM(NEW.contact_telephone_number) OR TRIM(contact_mobile_number) is not distinct from TRIM(NEW.contact_mobile_number)
                        ) AND record_status <> 'EXCEPTION'
                ) > 0
        THEN
                NEW.last_confirmed_food_delivery = (SELECT last_confirmed_food_delivery
                FROM resident_support_annex
                WHERE uprn = NEW.uprn
                ORDER BY last_confirmed_food_delivery DESC NULLS LAST LIMIT 1);
                -- Bring forward case note
                NEW.case_notes = (SELECT case_notes
                FROM resident_support_annex
                WHERE uprn = NEW.uprn
                ORDER BY last_confirmed_food_delivery DESC NULLS LAST LIMIT 1);
                -- Bring forward delivery note
                NEW.delivery_notes = (SELECT delivery_notes
                FROM resident_support_annex
                WHERE uprn = NEW.uprn
                ORDER BY last_confirmed_food_delivery DESC NULLS LAST LIMIT 1);
                -- Set ongoing food need to that of the most recent
                NEW.ongoing_food_need = (SELECT ongoing_food_need
                FROM resident_support_annex
                WHERE uprn = NEW.uprn
                ORDER BY last_confirmed_food_delivery DESC NULLS LAST LIMIT 1);
                --**************************************************************************************
                update resident_support_annex set is_duplicate = 'TRUE',
                        record_status = 'DUPLICATE',
                --*********************************** NEW CODE ****************************************
                        ongoing_food_need = false -- we need to set this to false to stop multiple records from haing this for the same person/household
                --**************************************************************************************
                        where TRIM(uprn) is not distinct from TRIM(NEW.uprn)
                AND TRIM(dob_day) is not distinct from TRIM(NEW.dob_day)
                AND TRIM(dob_month) is not distinct from TRIM(NEW.dob_month)
                AND TRIM(dob_year) is not distinct from TRIM(NEW.dob_year)
                AND (TRIM(contact_telephone_number) is not distinct from TRIM(NEW.contact_telephone_number) OR TRIM(contact_mobile_number) is not distinct from TRIM(NEW.contact_mobile_number));
                NEW.is_duplicate = 'FALSE';
                NEW.record_status = 'MASTER';
        ELSEIF(SELECT count(id)
                FROM resident_support_annex
                WHERE TRIM(uprn) is not distinct from TRIM(NEW.uprn)) > 0 THEN
                NEW.is_duplicate = 'TRUE';
                NEW.record_status = 'EXCEPTION';
        ELSE
                NEW.is_duplicate = 'FALSE';
                NEW.record_status = 'MASTER';
        END IF;
        RETURN NEW;
END;
$function1$
;

CREATE TRIGGER annex_insert_trigger BEFORE INSERT ON public.resident_support_annex FOR EACH ROW EXECUTE PROCEDURE public.set_duplicate_flag();

CREATE FUNCTION public.set_last_delivery_date()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function2$
DECLARE food_need BOOLEAN := NEW.ongoing_food_need;
BEGIN
        IF NEW.record_status <> OLD.record_status AND NEW.record_status = 'MASTER'
        THEN
                NEW.last_confirmed_food_delivery = (SELECT last_confirmed_food_delivery
                FROM resident_support_annex
                WHERE uprn = NEW.uprn AND id <> NEW.id
                ORDER BY last_confirmed_food_delivery DESC NULLS LAST LIMIT 1);
                update resident_support_annex set
                ongoing_food_need = false,
                record_status = 'DUPLICATE',
                is_duplicate = true
                where TRIM(uprn) is not distinct from TRIM(NEW.uprn)
                AND TRIM(dob_day) is not distinct from TRIM(NEW.dob_day)
                AND TRIM(dob_month) is not distinct from TRIM(NEW.dob_month)
                AND TRIM(dob_year) is not distinct from TRIM(NEW.dob_year)
                AND (TRIM(contact_telephone_number) is not distinct from TRIM(NEW.contact_telephone_number) OR TRIM(contact_mobile_number) is not distinct from TRIM(NEW.contact_mobile_number))
                AND id <> NEW.id;
        NEW.ongoing_food_need = food_need;
        END IF;
        RETURN NEW;
END;
$function2$
;
CREATE TRIGGER update_trigger BEFORE UPDATE ON public.resident_support_annex FOR EACH ROW EXECUTE PROCEDURE public.set_last_delivery_date();

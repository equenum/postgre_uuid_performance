start transaction;

create table if not exists monitor.targets_v4 (
    id uuid primary key,
    resource_id uuid,
    display_name text not null,
    description text,
    url text not null,
    cron_schedule text not null,
    change_type text not null,
    html_tag text not null,
    selector_type text not null,
    selector_value text not null,
    expected_value text,
    created_at timestamp not null,
    updated_at timestamp
);
	
commit;

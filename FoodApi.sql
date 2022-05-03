create database FoodApi;
use FoodApi;
create table Ingredient(
	IngredientId int not null auto_increment,
    IngredientNmae varchar(1000) not null,
    Protein int default null,
    Carbs int default null,
    primary key (IngredientId)
);
create table Food (
	FoodId int not null auto_increment,
    FoodName varchar(1000) not null,
    GlutenFree bool default null,
    Vegan bool default null,
    primary key (FoodId)
);
alter table Food add column IngredientId int;
alter table Food add constraint FK_Ingredient foreign key (IngredientId) references Ingredient(IngredientId);
alter table Food add column Protein int default null;
alter table Food add column Carbs int default null;
alter table Ingredient add column ServingSize double not null;
alter table Ingredient add column ServingSizeUnit varchar(1000) not null;
alter table Ingredient drop column protein;
alter table Ingredient drop column carbs;
insert into Ingredient (IngredientNmae, ServingSize, ServingSizeUnit) values ("Chicken", 1, "sandwich");
insert into Food (FoodName, IngredientId, protein, carbs) values ("popeyes chicken sandwich", 1, 28, 50);
alter table food modify protein double default null;
alter table food modify carbs double default null;
alter table Food modify GlutenFree bool default false;
alter table Food modify Vegan bool default false;
alter table ingredient drop column IngredientNmae;
alter table ingredient add IngredientName varchar(1000) not null;
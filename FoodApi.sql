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
insert into Ingredient (IngredientName, ServingSize, ServingSizeUnit) values ("flour", 49, "gram");
insert into Food (FoodName, IngredientId, protein, carbs) values ("original glazed doughnut", 2, 3, 21);
insert into Ingredient (IngredientName, ServingSize, ServingSizeUnit) values ("corn", 1, "ear");
insert into Food (FoodName, IngredientId, Protein, carbs, Vegan, GlutenFree) values ("corn", 3,3,19,true,true);
insert into Ingredient (IngredientName,servingSize,ServingSizeUnit) values ("spaghetti",2,"oz");
insert into Food (FoodName,IngredientId, protein, carbs, vegan) values ("spaghetti and vegan meatball",4,8,53.6,true);
create index healthy on food (vegan, glutenfree);
alter table food drop index healthy;
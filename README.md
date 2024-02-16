# resources
For RageMP

Есть система автомобилей, она позволяет игрокам покупать/продавать автомобили, а администраторам бесплатно получать временные авто, администратор появляется за рулем и если выйдет из машины, то авто удаляется, к нему никто не может сесть, машина только для него. Для игроков с автомобилями существуют команды: установка точки спавна автомобиля, продажа автомобиля другому игроку (Id игрока, Id автомобиля который продается, цена), продажа автомобиля государству по гос. стоимости. Для всех игроков существуют команды: купить автомобиль (его название, цвет), принять предложение о покупки автомобиля от другого игрока.
При покупке автомобиля, автомобилю присваивается 5 значный номер из случайных букв и цифр. У автомобилей администрации номера всегда ADMIN.
Купленные автомобили появляются в мире только когда игрок заходит в игру.

Оценивается все, соблюдение одного стиля в проекте, нейминги классов, полей, свойств и т.п.
Если требуется реализовать какие-то вспомогательные системы, то они также будут оцениваться и могут принести положительную оценку.
Проектирование базы данных по "стандарту" Code First.

Стек технологий: .NET Core 3.1, EF Core, Postgres.

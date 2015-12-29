var data = (function (data) {

    //Contructor
    function Driver(firstName, lastName) {
        this.firstName = firstName;
        this.lastName = lastName;
    }

    data.drivers = [
      new Driver('Koen', 'Steurs'),
      new Driver('Evelien', 'Schepers')
    ];

    return data;
})(data || {});
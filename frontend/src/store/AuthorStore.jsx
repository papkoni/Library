import { makeAutoObservable } from "mobx";

export default class AuthorStore {
    constructor() {
        this._authors = [
            { 
                id: 'some-guid-1', 
                firstName: 'John', 
                surname: 'Doe', 
                birthday: new Date(1970, 1, 1), 
                country: 'USA' 
            },
            { 
                id: 'some-guid-2', 
                firstName: 'Jane', 
                surname: 'Smith', 
                birthday: new Date(1980, 2, 2), 
                country: 'UK' 
            },
        ];
        this._selectedAuthor = {}

        makeAutoObservable(this);
    }

     

    setAuthors(authors){
        this._authors = authors;
    }

    setSelectedAuthor(author) {
        this._selectedAuthor = author
    }


    get authors() {
        return this._authors;
    }

    get selectedAuthor(){
        return this._selectedAuthor;
    }
}

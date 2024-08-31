import { makeAutoObservable } from "mobx";

export default class BookStore {
    constructor() {
        this._books = [
            { 
                id: 'book-guid-1', 
                title: 'Book Title 1', 
                isbn: '1234567890', 
                description: 'This is a book description', 
                recieveDate: new Date(2021, 6, 15), 
                returnDate: new Date(2021, 7, 15), 
                genre: 'Fiction', 
                authorId: 'some-guid-1', 
                imageName: '\\Users\\nikitapapko\\ModsenProject\\Library\\frontend\\src\\store\\image1.png', 
                userId: 'user-guid-1'
            },
            { 
                id: 'book-guid-2', 
                title: 'Book Title 2', 
                isbn: '0987654321', 
                description: 'This is another book description', 
                recieveDate: new Date(2021, 7, 20), 
                returnDate: new Date(2021, 8, 20), 
                genre: 'Non-Fiction', 
                authorId: 'some-guid-2', 
                imageName: '/Users/nikitapapko/ModsenProject/Library/frontend/src/store/image1.png', 
                userId: null 
            },
            { 
                id: 'book-guid-3', 
                title: 'Book Title 2', 
                isbn: '0987654321', 
                description: 'This is another book description', 
                recieveDate: new Date(2021, 7, 20), 
                returnDate: new Date(2021, 8, 20), 
                genre: 'Non-Fiction', 
                authorId: 'some-guid-2', 
                imageName: '/Users/nikitapapko/ModsenProject/Library/frontend/src/store/image1.png', 
                userId: null 
            },
            { 
                id: 'book-guid-4', 
                title: 'Book Title 2', 
                isbn: '0987654321', 
                description: 'This is another book description', 
                recieveDate: new Date(2021, 7, 20), 
                returnDate: new Date(2021, 8, 20), 
                genre: 'Non-Fiction', 
                authorId: 'some-guid-2', 
                imageName: '/Users/nikitapapko/ModsenProject/Library/frontend/src/store/image1.png', 
                userId: null 
            },
            { 
                id: 'book-guid-5', 
                title: 'Book Title 2', 
                isbn: '0987654321', 
                description: 'This is another book description', 
                recieveDate: new Date(2021, 7, 20), 
                returnDate: new Date(2021, 8, 20), 
                genre: 'Non-Fiction', 
                authorId: 'some-guid-2', 
                imageName: '/Users/nikitapapko/ModsenProject/Library/frontend/src/store/image1.png', 
                userId: null 
            },
            
            
        ];
        
        makeAutoObservable(this);
    }
 
    setBooks(books){
        this._books = books;
    }

    get books() {
        return this._books;
    }
}



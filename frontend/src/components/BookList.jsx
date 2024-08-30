import React, {useContext} from 'react';
import {observer} from "mobx-react-lite";
import {Context} from "../main";
import { Row } from 'react-bootstrap';
import BookItem from "./BookItem";

const BookList = observer(() => {
    const {book} = useContext(Context)
    return (
        <Row>
            {book.books.map(book =>
                <BookItem key={book.id} book={book}/>
            )}
        </Row>
        
    );
});

export default BookList;
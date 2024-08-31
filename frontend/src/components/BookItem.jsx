import React, {useContext} from 'react';
import {Context} from "../main";
import { Card, Col, Image } from 'react-bootstrap';
import {useNavigate} from "react-router-dom"
import { BOOK_ROUTE } from '../utils/consts';

const BookItem = ({book}) => {
    const navigateTo = useNavigate()
    return (
        <Col md={3} className={"mt-3"} onClick={() => navigateTo(BOOK_ROUTE + '/' + book.id)}>
            <Card style={{width: 150, cursor: 'pointer'}} border={"light"}>
                <Image width={150} height={150} src={book.imageName}/>
                
                <div>{book.title}</div>
            </Card>
        </Col>
        
    );
};

export default BookItem;
import React, {useContext} from 'react';
import {Context} from "../main";
import { Card, Col, Image } from 'react-bootstrap';

const BookItem = ({book}) => {
    return (
        <Col md={3} className={"mt-3"}>
            <Card style={{width: 150, cursor: 'pointer'}} border={"light"}>
                <Image width={150} height={150} src={book.imageName}/>
                
                <div>{book.title}</div>
            </Card>
        </Col>
        
    );
};

export default BookItem;
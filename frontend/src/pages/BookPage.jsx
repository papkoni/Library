import React from 'react';
import {Button, Card, Col, Container, Image, Row} from "react-bootstrap";

const DevicePage = () => {
    const book = { 
        id: 'book-guid-1', 
        title: 'Book Title 1', 
        isbn: '1234567890', 
        description: 'This is a book description', 
        recieveDate: new Date(2021, 6, 15), 
        returnDate: new Date(2021, 7, 15), 
        genre: 'Fiction', 
        authorId: 'some-guid-1', 
        imageName: '/Users/nikitapapko/ModsenProject/Library/frontend/src/store/image1.png', 
        userId: 'user-guid-1'
    }

    return (
        <Container className="mt-3">
            <Row>
                <Col md={4}>
                    <Image width={300} height={300} src={ book.imageName}/>
                </Col>
                
                <Col md={4}>
                    <Card
                        className="d-flex flex-column align-items-center justify-content-around"
                        style={{width: 300, height: 300, fontSize: 32, border: '5px solid lightgray'}}
                    >
                        
                        <Button variant={"outline-dark"}>Добавить на полку</Button>
                    </Card>
                </Col>
            </Row>
            <Row>
                <h2>
                {book.title}
                </h2>
            </Row>
            <Row className="d-flex flex-column m-3">
                <h1>О книге</h1>
                
                    <Row style={{background:  'transparent', padding: 10}}>
                        {book.description}
                    </Row>
               
            </Row>
        </Container>
    );
};

export default DevicePage;
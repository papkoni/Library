import React, {useContext, useEffect} from 'react';
import {Container} from "react-bootstrap";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import AuthorBar from "../components/AuthorBar";
import {observer} from "mobx-react-lite";
import {Context} from "../main";
import BookList from "../components/BookList";


const Library = observer(() => {
    const {book} = useContext(Context)
    return (
        <Container >
            <Row className="mt-2">
                <Col md={3}>
                    <AuthorBar/>
                </Col>
                <Col md={9}>
                  <BookList></BookList>  
                 
                </Col>
            </Row>

        </Container>
    )
});


export default Library;

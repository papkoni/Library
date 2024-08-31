import React, {useState} from 'react';
import {Button, Container} from "react-bootstrap";
import CreateBook from "../components/modals/CreateBook";
import CreateAuthor from "../components/modals/CreateAuthor";

const Admin = () => {
    const [bookVisible, setBookVisible] = useState(false)
    const [authorVisible, setAuthorVisible] = useState(false)

    return (
        <Container className="d-flex flex-column">
            <Button
                variant={"outline-dark"}
                className="mt-4 p-2"
                onClick={() => setBookVisible(true)}
            >
                Добавить книгу
            </Button>
            <Button
                variant={"outline-dark"}
                className="mt-4 p-2"
                onClick={() => setAuthorVisible(true)}
            >
                Добавить автора
            </Button>
            
            <CreateAuthor show={authorVisible} onHide={() => setAuthorVisible(false)}/>
            <CreateBook show={bookVisible} onHide={() => setBookVisible(false)}/>
        </Container>
    );
};

export default Admin;
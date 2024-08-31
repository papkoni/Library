import React, {useContext} from 'react';
import {observer} from "mobx-react-lite";
import {Context} from "../main";
import ListGroup from "react-bootstrap/ListGroup";

const AuthorBar = observer(() => {
    
    const {author} = useContext(Context)
    return (
        <ListGroup>
            {author.authors.map(a =>
                <ListGroup.Item
                    style={{cursor: 'pointer'}}
                    active={a.id === author.selectedAuthor.id}
                    onClick={() => author.setSelectedAuthor(a)}
                    key={a.id}
                >
                    {a.surname}
                </ListGroup.Item>
            )}
        </ListGroup>
    );
});

export default AuthorBar;
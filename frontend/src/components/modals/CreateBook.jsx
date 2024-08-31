import React, {useContext, useEffect, useState} from 'react';
import Modal from "react-bootstrap/Modal";
import {Button, Dropdown, Form, Row, Col} from "react-bootstrap";
import {Context} from "../../main";
import {observer} from "mobx-react-lite";

const CreateBook = observer(({show, onHide}) => {
    const {author} = useContext(Context)
    const [selectedAuthor, setSelectedAuthor] = useState(null);

    const handleSelect = (a) => {
        setSelectedAuthor(a);
        author.setSelectedAuthor(a); // Если вам нужно сохранять выбранного автора в глобальном состоянии
    };
    // const [name, setName] = useState('')
    // const [price, setPrice] = useState(0)
    // const [file, setFile] = useState(null)
    // const [info, setInfo] = useState([])

    // useEffect(() => {
    //     fetchTypes().then(data => device.setTypes(data))
    //     fetchBrands().then(data => device.setBrands(data))
    // }, [])

    // const addInfo = () => {
    //     setInfo([...info, {title: '', description: '', number: Date.now()}])
    // }
    // const removeInfo = (number) => {
    //     setInfo(info.filter(i => i.number !== number))
    // }
    // const changeInfo = (key, value, number) => {
    //     setInfo(info.map(i => i.number === number ? {...i, [key]: value} : i))
    // }

    // const selectFile = e => {
    //     setFile(e.target.files[0])
    // }

    // const addDevice = () => {
    //     const formData = new FormData()
    //     formData.append('name', name)
    //     formData.append('price', `${price}`)
    //     formData.append('img', file)
    //     formData.append('brandId', device.selectedBrand.id)
    //     formData.append('typeId', device.selectedType.id)
    //     formData.append('info', JSON.stringify(info))
    //     createDevice(formData).then(data => onHide())
    // }

    return (
        <Modal
            show={show}
            onHide={onHide}
            centered
        >
            <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-vcenter">
                    Добавить книгу
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form>

                    <Form.Control
                        // value={name}
                        // onChange={e => setName(e.target.value)}
                        className="mt-3"
                        placeholder="Введите названии книги"
                    />
                           

                    <Dropdown className="mt-2 mb-2">
                        <Dropdown.Toggle>{selectedAuthor ? `${selectedAuthor.surname} ${selectedAuthor.firstName}` : "Выберите автора"}</Dropdown.Toggle>
                        <Dropdown.Menu>
                            {author.authors.map(a =>
                                <Dropdown.Item
                                    onClick={() => handleSelect(a)}
                                    key={a.id}
                                >
                                    {a.surname + " " + a.firstName}
                                </Dropdown.Item>
                            )}
                        </Dropdown.Menu>
                    </Dropdown>
            
                   

                    

                    <Form.Control
                        // value={price}
                        // onChange={e => setPrice(Number(e.target.value))}
                        className="mt-3"
                        placeholder="Введите ISBN"
                        type="number"

                    />
                    <Form.Control
                        className="mt-3"
                        placeholder="Введите жанр"
                        // onChange={selectFile}
                    />
                    
                    <Form.Control
                        className="mt-3"
                        as="textarea" // Указываем, что это многострочное текстовое поле
                        placeholder="Введите текст"
                        style={{ height: '100px', padding: '10px', textAlign: 'left' }}
/>
                    
                </Form>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="outline-danger" onClick={onHide}>Закрыть</Button>
                <Button variant="outline-success">Добавить</Button>
            </Modal.Footer>
        </Modal>
    );
});

export default CreateBook;
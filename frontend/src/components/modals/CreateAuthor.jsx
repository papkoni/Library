import React, {useContext, useEffect, useState} from 'react';
import Modal from "react-bootstrap/Modal";
import {Button, Dropdown, Form, Row, Col} from "react-bootstrap";
import {Context} from "../../main";
import {observer} from "mobx-react-lite";

const CreateAuthor = observer(({show, onHide}) => {
    const {author} = useContext(Context)
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
                    Добавить автора
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form>
                    
                    <Form.Control
                        // value={name}
                        // onChange={e => setName(e.target.value)}
                        className="mt-3"
                        placeholder="Введите имя автора"
                    />

                    <Form.Control
                        // value={name}
                        // onChange={e => setName(e.target.value)}
                        className="mt-3"
                        placeholder="Введите фамилию автора"
                    />

                    <Form.Control
                        // value={name}
                        // onChange={e => setName(e.target.value)}
                        className="mt-3"
                        placeholder="Введите фамилию автора"

                    />




                    <Form.Control
                        // value={price}
                        // onChange={e => setPrice(Number(e.target.value))}
                        className="mt-3"
                        placeholder="Введите дату рождения автора"
                        type="date"

                    />
                    <Form.Control
                        className="mt-3"
                        placeholder="Введите страну автора"
                        // onChange={selectFile}
                    />
                    
                    
                    
                </Form>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="outline-danger" onClick={onHide}>Закрыть</Button>
                <Button variant="outline-success" >Добавить</Button>
            </Modal.Footer>
        </Modal>
    );
});

export default CreateAuthor;
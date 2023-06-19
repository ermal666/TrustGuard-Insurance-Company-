import { Space, Table, Typography, Modal, Input, Button } from "antd";
import { useEffect, useState, useRef } from "react";
import { getTpl } from "../../API";
import { deleteTpl, updateTpl, addTpl } from "./delete";
import { EditOutlined, DeleteOutlined, PlusOutlined } from "@ant-design/icons";

function Tpl() {
  const [loading, setLoading] = useState(false);
  const [dataSource, setDataSource] = useState([]);
  const [isEditing, setIsEditing] = useState(false);
  const [editingTpl, setEditingTpl] = useState(null);
  const [deleteModalVisible, setDeleteModalVisible] = useState(false);
  const [deletingTpl, setDeletingTpl] = useState(null);
  const [addModalVisible, setAddModalVisible] = useState(false);
  const [newTpl, setNewTpl] = useState({
    PolicyNumber: "",
    PolicyValidation: "",
   VindNumber: "",
    Plate: "",
   Producer: "",
  PurchaseDate: "",
   Offer:"",
     
  });

  const firstNameInputRef = useRef(null);
  useEffect(() => {
    setLoading(true);
    getTpl()
      .then((res) => {
        setDataSource(res.users);
        setLoading(false);
      })
      .catch((error) => {
        // Handle error
        setLoading(false);
      });
  }, []);

  const handleEdit = (record) => {
    setEditingTpl(record);
    setIsEditing(true);
  };

  const handleCancelEdit = () => {
    setIsEditing(false);
    setEditingTpl(null);
  };

  const handleSave = async () => {
    try {
      // Perform API call to update the user
      await updateTpl(editingTpl);
      // Update the dataSource with the edited user
      setDataSource((prevDataSource) =>
        prevDataSource.map((tpl) =>
          tpl.id === editingTpl.id ? editingTpl : tpl
        )
      );
      // Reset editing state
      setIsEditing(false);
      setEditingTpl(null);
    } catch (error) {
      // Handle error
    }
  };

  const onDeleteTpl = (record) => {
    setDeletingTpl(record);
    setDeleteModalVisible(true);
  };

  const handleDeleteModalOk = async () => {
    try {
      await deleteTpl(deletingTpl.id);
      setDataSource((prevDataSource) =>
        prevDataSource.filter((tpl) => tpl.id !== deletingTpl.id)
      );
      setDeleteModalVisible(false);
      setDeletingTpl(null);
    } catch (error) {
      // Handle error
    }
  };

  const handleDeleteModalCancel = () => {
    setDeleteModalVisible(false);
    setDeletingTpl(null);
  };

  const handleAdd = () => {
    setAddModalVisible(true);
  };

  const handleAddModalOk = async () => {
    try {
      // Perform API call to add the user
      const addedTpl = await addTpl(newTpl);
      // Update the dataSource with the added user
      setDataSource((prevDataSource) => [...prevDataSource, addedTpl]);

      setAddModalVisible(false);
      setNewTpl({
        PolicyNumber: "",
    PolicyValidation: "",
   VindNumber: "",
    Plate: "",
   Producer: "",
  PurchaseDate: "",
   Offer:"",
      });
    } catch (error) {
      // Handle error
    }
  };

  const handleAddModalCancel = () => {
    setAddModalVisible(false);
    setNewTpl({
      PolicyNumber: "",
      PolicyValidation: "",
     VindNumber: "",
      Plate: "",
     Producer: "",
    PurchaseDate: "",
     Offer:"",
    });
  };

  const columns = [
    {
      title: "Id",
      dataIndex: "id",
    },
    {
      title: "Policy Number",
      dataIndex: "PolicyNumber",
    },
    {
      title: "Policy Validation",
      dataIndex: "PolicyValidation",
    },
    {
      title: "Vin Number",
      dataIndex: "VinNumber",
    },
    {
      title: "Plate",
      dataIndex: "Plate",
    },
    {
      title: "Producer",
      dataIndex: "Producer",
    },
    {
        title: "Purchase Date",
        dataIndex: "PurchaseDate",
      },
      {
        title: "Offer",
        dataIndex: "Offer",
      },
     
    {
      key: "actions",
      title: "Actions",
      render: (record) => (
        <>
          <EditOutlined onClick={() => handleEdit(record)} />
          <DeleteOutlined
            onClick={() => onDeleteTpl(record)}
            style={{ color: "red", marginLeft: 12 }}
          />
        </>
      ),
    },
  ];

  return (
    <Space size={20} direction="vertical">
      <Typography.Title level={4}>TPL Insurance</Typography.Title>
      <Button type="primary" icon={<PlusOutlined />} onClick={handleAdd}>
        Add TPL Insurance
      </Button>
      <Table
        loading={loading}
        columns={columns}
        dataSource={dataSource}
        pagination={{ pageSize: 5 }}
      />
      {isEditing && (
        <Modal
          title="Edit TPL Insurance"
          visible={isEditing}
          onCancel={handleCancelEdit}
          footer={[
            <Button key="cancel" onClick={handleCancelEdit}>
              Cancel
            </Button>,
            <Button key="save" type="primary" onClick={handleSave}>
              Save
            </Button>,
          ]}
        >
          {/* Add input fields for editing user details */}
          <Input
            style={{ margin: "5px" }}
            value={editingTpl?.PolicyNumber}
            onChange={(e) =>
              setEditingTpl((prevTpl) => ({
                ...prevTpl,
                PolicyNumber: e.target.value,
              }))
            }
          />
          <Input
            style={{ margin: "5px" }}
            value={editingTpl?.PolicyValidation}
            onChange={(e) =>
              setEditingTpl((prevTpl) => ({
                ...prevTpl,
                PolicyValidation: e.target.value,
              }))
            }
            />
            <Input
            style={{ margin: "5px" }}
            value={editingTpl?.VinNumber}
            onChange={(e) =>
              setEditingTpl((prevTpl) => ({
                ...prevTpl,
                VinNumberNumber: e.target.value,
              }))
            }
          />
        <Input
            style={{ margin: "5px" }}
            value={editingTpl?.Plate}
            onChange={(e) =>
              setEditingTpl((prevTpl) => ({
                ...prevTpl,
                Plate: e.target.value,
              }))
            }
          />
        <Input
            style={{ margin: "5px" }}
            value={editingTpl?.Producer}
            onChange={(e) =>
              setEditingTpl((prevTpl) => ({
                ...prevTpl,
                Producer: e.target.value,
              }))
            }
          />
          <Input
            style={{ margin: "5px" }}
            value={editingTpl?.PurchaseDate}
            onChange={(e) =>
              setEditingTpl((prevTpl) => ({
                ...prevTpl,
                PurchaseDate: e.target.value,
              }))
            }
          />
          <Input
            style={{ margin: "5px" }}
            value={editingTpl?.Offer}
            onChange={(e) =>
              setEditingTpl((prevTpl) => ({
                ...prevTpl,
                Offer: e.target.value,
              }))
            }
          />
          
        </Modal>
      )}
      <Modal
        title="Delete TPL Insurance"
        visible={deleteModalVisible}
        onOk={handleDeleteModalOk}
        onCancel={handleDeleteModalCancel}
        okText="Delete"
        cancelText="Cancel"
      >
        <p>Are you sure you want to delete this TPL insurance?</p>
      </Modal>
      <Modal
        title="Add TPL Insurance"
        visible={addModalVisible}
        onOk={handleAddModalOk}
        onCancel={handleAddModalCancel}
        okText="Add"
        cancelText="Cancel"
      >
        {/* Add input fields for adding a new user */}
        <Input
          style={{ margin: "5px" }}
          placeholder="Policy Number"
          value={newTpl.PolicyNumber}
          onChange={(e) =>
            setNewTpl((prevTpl) => ({
              ...prevTpl,
            PolicyNumber: e.target.value,
            }))
          }
        />
          <Input
          style={{ margin: "5px" }}
          placeholder="Policy Validation"
          value={newTpl.PolicyValidation}
          onChange={(e) =>
            setNewTpl((prevTpl) => ({
              ...prevTpl,
             PolicyValidation: e.target.value,
            }))
          }
        />
         <Input
          style={{ margin: "5px" }}
          placeholder="Vin Number"
          value={newTpl.VindNumber}
          onChange={(e) =>
            setNewTpl((prevTpl) => ({
              ...prevTpl,
             VinNumber: e.target.value,
            }))
          }
        />
         <Input
          style={{ margin: "5px" }}
          placeholder="Plate"
          value={newTpl.Plate}
          onChange={(e) =>
            setNewTpl((prevTpl) => ({
              ...prevTpl,
             Plate: e.target.value,
            }))
          }
        />
         <Input
          style={{ margin: "5px" }}
          placeholder="Producer"
          value={newTpl.Producer}
          onChange={(e) =>
            setNewTpl((prevTpl) => ({
              ...prevTpl,
             Producer: e.target.value,
            }))
          }
        />
        <Input
          style={{ margin: "5px" }}
          placeholder="Purchase Date"
          value={newTpl.PurchaseDate}
          onChange={(e) =>
            setNewTpl((prevTpl) => ({
              ...prevTpl,
             PurchaseDate: e.target.value,
            }))
          }
        />
        <Input
          style={{ margin: "5px" }}
          placeholder="Offer"
          value={newTpl.Offer}
          onChange={(e) =>
            setNewTpl((prevTpl) => ({
              ...prevTpl,
             Offer: e.target.value,
            }))
          }
        />
       
      </Modal>
    </Space>
  );
}

export default Tpl;

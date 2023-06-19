import { Space, Table, Typography, Modal, Input, Button } from "antd";
import { useEffect, useState, useRef } from "react";
import { getUsers } from "../../API";
import { deleteUser, updateUser, addUser } from "./delete";
import { EditOutlined, DeleteOutlined, PlusOutlined } from "@ant-design/icons";

function Users() {
  const [loading, setLoading] = useState(false);
  const [dataSource, setDataSource] = useState([]);
  const [isEditing, setIsEditing] = useState(false);
  const [editingUser, setEditingUser] = useState(null);
  const [deleteModalVisible, setDeleteModalVisible] = useState(false);
  const [deletingUser, setDeletingUser] = useState(null);
  const [addModalVisible, setAddModalVisible] = useState(false);
  const [newUser, setNewUser] = useState({
    PersonalId: "",
        FullName: "",
        birthDate: "",
        PhoneNumber: "",
          Address: "",
         
  
  });

  const firstNameInputRef = useRef(null);
  useEffect(() => {
    setLoading(true);
    getUsers()
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
    setEditingUser(record);
    setIsEditing(true);
  };

  const handleCancelEdit = () => {
    setIsEditing(false);
    setEditingUser(null);
  };

  const handleSave = async () => {
    try {
      // Perform API call to update the user
      await updateUser(editingUser);
      // Update the dataSource with the edited user
      setDataSource((prevDataSource) =>
        prevDataSource.map((user) =>
          user.id === editingUser.id ? editingUser : user
        )
      );
      // Reset editing state
      setIsEditing(false);
      setEditingUser(null);
    } catch (error) {
      // Handle error
    }
  };

  const onDeleteUser = (record) => {
    setDeletingUser(record);
    setDeleteModalVisible(true);
  };

  const handleDeleteModalOk = async () => {
    try {
      await deleteUser(deletingUser.id);
      setDataSource((prevDataSource) =>
        prevDataSource.filter((user) => user.id !== deletingUser.id)
      );
      setDeleteModalVisible(false);
      setDeletingUser(null);
    } catch (error) {
      // Handle error
    }
  };

  const handleDeleteModalCancel = () => {
    setDeleteModalVisible(false);
    setDeletingUser(null);
  };

  const handleAdd = () => {
    setAddModalVisible(true);
  };

  const handleAddModalOk = async () => {
    try {
      // Perform API call to add the user
      const addedUser = await addUser(newUser);
      // Update the dataSource with the added user
      setDataSource((prevDataSource) => [...prevDataSource, addedUser]);

      setAddModalVisible(false);
      setNewUser({
        PersonalId: "",
        FullName: "",
        birthDate: "",
        PhoneNumber: "",
        Address: "",
          
      });
    } catch (error) {
      // Handle error
    }
  };

  const handleAddModalCancel = () => {
    setAddModalVisible(false);
    setNewUser({
      PersonalId: "",
        FullName: "",
        birthDate: "",
        PhoneNumber: "",
        Address: "",
         
    });
  };

  const columns = [
    {
      title: "Id",
      dataIndex: "id",
    },
    {
      title: "Personal Id",
      dataIndex: "PersonalId",
    },
    {
      title: "Full Name",
      dataIndex: "FullName",
    },
    {
      title: "Birthday",
      dataIndex: "BirthDate",
    },
    
    {
      title: "Phone",
      dataIndex: "PhoneNumber",
    },
    {
      title: "Address",
      dataIndex: "Address",
    
    },
    {
      key: "actions",
      title: "Actions",
      render: (record) => (
        <>
          <EditOutlined onClick={() => handleEdit(record)} />
          <DeleteOutlined
            onClick={() => onDeleteUser(record)}
            style={{ color: "red", marginLeft: 12 }}
          />
        </>
      ),
    },
  ];

  return (
    <Space size={20} direction="vertical">
      <Typography.Title level={4}>Users</Typography.Title>
      <Button type="primary" icon={<PlusOutlined />} onClick={handleAdd}>
        Add User
      </Button>
      <Table
        loading={loading}
        columns={columns}
        dataSource={dataSource}
        pagination={{ pageSize: 5 }}
      />
      {isEditing && (
        <Modal
          title="Edit User"
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
            value={editingUser?.PersonalId}
            onChange={(e) =>
              setEditingUser((prevUser) => ({
                ...prevUser,
                PersonalId: e.target.value,
              }))
            }
          />
          <Input
            style={{ margin: "5px" }}
            value={editingUser?.FullName}
            onChange={(e) =>
              setEditingUser((prevUser) => ({
                ...prevUser,
                FullName: e.target.value,
              }))
            }
          />
          <Input
            style={{ margin: "5px" }}
            value={editingUser?.BirthDate}
            onChange={(e) =>
              setEditingUser((prevUser) => ({
                ...prevUser,
                BirthDate: e.target.value,
              }))
            }
          />
          
          <Input
            style={{ margin: "5px" }}
            value={editingUser?.PhoneNumber}
            onChange={(e) =>
              setEditingUser((prevUser) => ({
                ...prevUser,
                PhoneNumber: e.target.value,
              }))
            }
          />
          <Input
            style={{ margin: "5px" }}
            value={editingUser?.Address}
            onChange={(e) =>
              setEditingUser((prevUser) => ({
                   ...prevUser,
                  Address: e.target.value,
              }))
            }
          />
          
        </Modal>
      )}
      <Modal
        title="Delete User"
        visible={deleteModalVisible}
        onOk={handleDeleteModalOk}
        onCancel={handleDeleteModalCancel}
        okText="Delete"
        cancelText="Cancel"
      >
        <p>Are you sure you want to delete this user?</p>
      </Modal>
      <Modal
        title="Add User"
        visible={addModalVisible}
        onOk={handleAddModalOk}
        onCancel={handleAddModalCancel}
        okText="Add"
        cancelText="Cancel"
      >
        {/* Add input fields for adding a new user */}
        <Input
          style={{ margin: "5px" }}
          placeholder="Personal Id"
          value={newUser.PersonalId}
          onChange={(e) =>
            setNewUser((prevUser) => ({
              ...prevUser,
              PersonalId: e.target.value,
            }))
          }
        />
        <Input
          style={{ margin: "5px" }}
          placeholder="Full Name"
          value={newUser.FullName}
          onChange={(e) =>
            setNewUser((prevUser) => ({
              ...prevUser,
              FullName: e.target.value,
            }))
          }
        />
        <Input
          style={{ margin: "5px" }}
          placeholder="Birthday"
          value={newUser.BirthDate}
          onChange={(e) =>
            setNewUser((prevUser) => ({
              ...prevUser,
              BirthDate: e.target.value,
            }))
          }
        />
       
        <Input
          style={{ margin: "5px" }}
          placeholder="Phone"
          value={newUser.PhoneNumber}
          onChange={(e) =>
            setNewUser((prevUser) => ({
              ...prevUser,
              PhoneNumber: e.target.value,
            }))
          }
        />
        <Input
          style={{ margin: "5px" }}
          placeholder="Address"
          value={newUser.Address}
          onChange={(e) =>
            setNewUser((prevUser) => ({
              ...prevUser,
              
                Address: e.target.value,
              
            }))
          }
        />
        
      </Modal>
    </Space>
  );
}

export default Users;

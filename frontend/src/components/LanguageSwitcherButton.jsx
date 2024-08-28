import React from "react";
import { useTranslation } from "react-i18next";
import { Menu, Dropdown, Button } from "antd";
import { DownOutlined } from "@ant-design/icons";

const LanguageSwitcherButton = () => {
  const { i18n } = useTranslation();

  const changeLanguage = (lng) => {
    i18n.changeLanguage(lng);
  };

  const menu = (
    <Menu>
      <Menu.Item key="en" onClick={() => changeLanguage("en")}>
        English
      </Menu.Item>
      <Menu.Item key="tr" onClick={() => changeLanguage("tr")}>
        Türkçe
      </Menu.Item>
    </Menu>
  );

  return (
    <Dropdown overlay={menu} trigger={["click"]}>
      <Button>
        {i18n.language === "tr" ? "Türkçe" : "English"} <DownOutlined />
      </Button>
    </Dropdown>
  );
};

export default LanguageSwitcherButton;

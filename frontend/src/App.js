import React from 'react';
import './i18n';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import DashboardLayout from './layouts/DashboardLayout';
import Login from './pages/Login';
import Users from './pages/Users';
import RealEstateManagement from './pages/RealEstateManagement';
import Reports from './pages/Reports';
import AuthMiddleware from './middlewares/AuthMiddleware';
import AdminMiddleware from './middlewares/AdminMiddleware';
import CurrencyList from './pages/CurrencyList';
import CurrencyAdd from './pages/CurrencyAdd';
import CurrencyEdit from './pages/CurrencyEdit';
import RealEstateStatusList from './pages/RealEstateStatusList';
import RealEstateStatusAdd from './pages/RealEstateStatusAdd';
import RealEstateStatusEdit from './pages/RealEstateStatusEdit';
import DynamicFeatureList from './pages/DynamicFeatureList';
import DynamicFeatureAdd from './pages/DynamicFeatureAdd';
import DynamicFeatureEdit from './pages/DynamicFeatureEdit';
import FeatureCategoryList from './pages/FeatureCategoryList';
import FeatureCategoryAddEdit from './pages/FeatureCategoryAddEdit';
import FeatureList from './pages/FeatureList';
import FeatureAddEdit from './pages/FeatureAddEdit';
import RealEstateTypeList from './pages/RealEstateTypeList';
import RealEstateTypeAddEdit from './pages/RealEstateTypeAddEdit';
import RealEstateTypeFeatures from './pages/RealEstateTypeFeatures';
import RealEstateAdd from './pages/RealEstateAdd';
import RealEstateList from './pages/RealEstateList';
import Dashboard from './pages/Dashboard';
import WebsiteLayout from './layouts/WebsiteLayout';
import Logout from './middlewares/Logout';
import Register from './pages/Register';
import RealEstateDetail from './pages/RealEstateDetail';
import WebsiteRealEstateList from './pages/WebsiteRealEstateList';
import RealEstateEdit from './pages/RealEstateEdit';
import UserMiddleWare from './middlewares/UserMiddleware';

const App = () => {
  return (
    <React.Fragment>
      <BrowserRouter>
        <Routes>
          <Route path="/login" element={<Login />} />
          <Route path="/logout" element={<Logout></Logout>} />
          <Route path="/register" element={<Register></Register>} />
          <Route path="/" element={<WebsiteLayout><WebsiteRealEstateList /></WebsiteLayout>} />


          <Route path="/dashboard" element={<AuthMiddleware><AdminMiddleware><DashboardLayout ><Dashboard /></DashboardLayout></AdminMiddleware></AuthMiddleware>} />
          <Route path="/dashboard/real-estate" element={<AuthMiddleware><AdminMiddleware><DashboardLayout><RealEstateManagement /></DashboardLayout></AdminMiddleware></AuthMiddleware>} />
          <Route path="/dashboard/currency/list" element={<AuthMiddleware><AdminMiddleware><DashboardLayout><CurrencyList /></DashboardLayout></AdminMiddleware></AuthMiddleware>} />
          <Route path="/dashboard/currency/add" element={<AuthMiddleware><AdminMiddleware><DashboardLayout><CurrencyAdd /></DashboardLayout></AdminMiddleware></AuthMiddleware>} />
          <Route path="/dashboard/currency/edit/:id" element={<AuthMiddleware><AdminMiddleware><DashboardLayout><CurrencyEdit /></DashboardLayout></AdminMiddleware></AuthMiddleware>} />
          <Route path="/dashboard/real-estate-status/list" element={<AuthMiddleware><AdminMiddleware><DashboardLayout><RealEstateStatusList /></DashboardLayout></AdminMiddleware></AuthMiddleware>} />
          <Route path="/dashboard/real-estate-status/add" element={<AuthMiddleware><AdminMiddleware><DashboardLayout><RealEstateStatusAdd /></DashboardLayout></AdminMiddleware></AuthMiddleware>} />
          <Route path="/dashboard/real-estate-status/edit/:id" element={<AuthMiddleware><AdminMiddleware><DashboardLayout><RealEstateStatusEdit /></DashboardLayout></AdminMiddleware></AuthMiddleware>} />
          <Route path="/dashboard/dynamic-feature/list" element={<AuthMiddleware><AdminMiddleware><DashboardLayout><DynamicFeatureList /></DashboardLayout></AdminMiddleware></AuthMiddleware>} />
          <Route path="/dashboard/dynamic-feature/add" element={<AuthMiddleware><AdminMiddleware><DashboardLayout><DynamicFeatureAdd /></DashboardLayout></AdminMiddleware></AuthMiddleware>} />
          <Route path="/dashboard/dynamic-feature/edit/:id" element={<AuthMiddleware><AdminMiddleware><DashboardLayout><DynamicFeatureEdit /></DashboardLayout></AdminMiddleware></AuthMiddleware>} />
          <Route path="/dashboard/detail-feature-categories/list" element={<AuthMiddleware><AdminMiddleware><DashboardLayout><FeatureCategoryList /></DashboardLayout></AdminMiddleware></AuthMiddleware>} />
          <Route path="/dashboard/detail-feature-categories/add" element={<AuthMiddleware><AdminMiddleware><DashboardLayout><FeatureCategoryAddEdit /></DashboardLayout></AdminMiddleware></AuthMiddleware>} />
          <Route path="/dashboard/detail-feature-categories/edit/:id" element={<AuthMiddleware><AdminMiddleware><DashboardLayout><FeatureCategoryAddEdit /></DashboardLayout></AdminMiddleware></AuthMiddleware>} />
          <Route path="/dashboard/detail-feature-categories/:categoryId/features/list" element={<AuthMiddleware><AdminMiddleware><DashboardLayout><FeatureList /></DashboardLayout></AdminMiddleware></AuthMiddleware>} />
          <Route path="/dashboard/detail-feature-categories/:categoryId/features/add" element={<AuthMiddleware><AdminMiddleware><DashboardLayout><FeatureAddEdit /></DashboardLayout></AdminMiddleware></AuthMiddleware>} />
          <Route path="/dashboard/detail-feature-categories/:categoryId/features/edit/:id" element={<AuthMiddleware><AdminMiddleware><DashboardLayout><FeatureAddEdit /></DashboardLayout></AdminMiddleware></AuthMiddleware>} />

          <Route path="/dashboard/real-estate-types/list" element={<AuthMiddleware><AdminMiddleware><DashboardLayout><RealEstateTypeList /></DashboardLayout></AdminMiddleware></AuthMiddleware>} />
          <Route path="/dashboard/real-estate-types/add" element={<AuthMiddleware><AdminMiddleware><DashboardLayout><RealEstateTypeAddEdit /></DashboardLayout></AdminMiddleware></AuthMiddleware>} />
          <Route path="/dashboard/real-estate-types/edit/:id" element={<AuthMiddleware><AdminMiddleware><DashboardLayout><RealEstateTypeAddEdit /></DashboardLayout></AdminMiddleware></AuthMiddleware>} />
          <Route path="/dashboard/real-estate-types/:id/features" element={<AuthMiddleware><AdminMiddleware><DashboardLayout><RealEstateTypeFeatures /></DashboardLayout></AdminMiddleware></AuthMiddleware>} />

          <Route path="/dashboard/real-estates/add" element={<AuthMiddleware><AdminMiddleware><DashboardLayout><RealEstateAdd /></DashboardLayout></AdminMiddleware></AuthMiddleware>} />
          <Route path="/dashboard/real-estates/edit/:id" element={<AuthMiddleware><AdminMiddleware><DashboardLayout><RealEstateEdit /></DashboardLayout></AdminMiddleware></AuthMiddleware>} />
          <Route path="/dashboard/real-estates/:id" element={<AuthMiddleware><AdminMiddleware><DashboardLayout><RealEstateDetail /></DashboardLayout></AdminMiddleware></AuthMiddleware>} />
          <Route path="/dashboard/real-estates/list" element={<AuthMiddleware><AdminMiddleware><DashboardLayout><RealEstateList /></DashboardLayout></AdminMiddleware></AuthMiddleware>} />


          <Route path="/real-estates/:id" element={<WebsiteLayout><RealEstateDetail /></WebsiteLayout>} />
          <Route path="/post-ad" element={<AuthMiddleware><UserMiddleWare> <WebsiteLayout><RealEstateAdd /></WebsiteLayout></UserMiddleWare></AuthMiddleware>} />
          <Route path="/my/real-estates" element={<AuthMiddleware><UserMiddleWare> <WebsiteLayout><RealEstateList /></WebsiteLayout></UserMiddleWare></AuthMiddleware>} />
          <Route path="/my/real-estates/edit/:id" element={<AuthMiddleware><UserMiddleWare> <WebsiteLayout><RealEstateEdit /></WebsiteLayout></UserMiddleWare></AuthMiddleware>} />






        </Routes>
      </BrowserRouter>
    </React.Fragment >
  )
}

export default App;

import { OrderStatus } from "./types/order-status";
import { UserRole } from "./types/role";

export interface AuditableEntity {
    active: boolean;
    creationDate: string;
    updateDate: string;
}

export interface Business extends AuditableEntity {
    id: number;
    code: string;
    name: string;
    description: string;
    email: string;
    address: string;
    city: string;
    country: string;
    logoUrl: string;
    template: string;
    useTemplate: boolean;
}

export interface Category extends AuditableEntity {
    id: number;
    parentId: number | null;
    businessId: number;
    code: string;
    name: string;
    description: string;
    imageUrl: string;
    business?: Business | null;
    parent?: Category | null;
}

export interface Product extends AuditableEntity {
    id: number;
    businessId: number;
    categoryId: number;
    code: string;
    brand: string;
    name: string;
    description: string;
    tax: number | null;
    discount: number | null;
    price: number;
    stock: number;
    imageUrl: string;
    business?: Business | null;
    category?: Category | null;
}

export interface Customer extends AuditableEntity {
    id: number;
    userId: number;
    businessId: number;
    code: string;
    dni: string;
    firstName: string;
    lastName: string;
    phone: string;
    address: string;
    business?: Business | null;
}

export interface PaymentMethod extends AuditableEntity {
    id: number;
    businessId: number;
    code: string;
    name: string;
    description: string;
    business?: Business | null;
}

export interface Order extends AuditableEntity {
    id: number;
    businessId: number;
    customerId: number;
    paymentMethodId: number;
    status: OrderStatus;
    date: string;
    total: number;
    subTotal: number;
    tax: number;
    discount: number;
    deliveryAddress: string;
    notes: string | null;
    business?: Business | null;
    customer?: Customer | null;
    paymentMethod?: PaymentMethod | null;
    orderDetails?: OrderDetail[];
}

export interface OrderDetail extends AuditableEntity {
    id: number;
    orderId: number;
    productId: number;
    quantity: number;
    unitPrice: number;
    total: number;
    subTotal: number;
    tax: number;
    discount: number;
    order?: Order | null;
    product?: Product | null;
}

export interface ShoppingCart extends AuditableEntity {
    id: number;
    businessId: number;
    customerId: number;
    code: string;
    payload: Record<string, unknown>;
    status: OrderStatus;
    business?: Business | null;
    customer?: Customer | null;
}

export interface User extends AuditableEntity {
    id: number;
    businessId: number;
    code: string;
    role: UserRole;
    email: string;
    password?: string;
    business?: Business | null;
    customers?: Customer[];
}

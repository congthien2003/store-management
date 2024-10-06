
import { User } from "./User";

export interface Store{
    id: number;
    name: string;
    address: string;
    phone: string;
    User: User;
}
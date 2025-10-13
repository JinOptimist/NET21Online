export interface Idol {
	id: number;
	name?: string | null;
	url?: string | null;
}

export interface CreateIdolRequest {
	name: string;
	url: string;
}

export interface ApiResponse<T> {
	data: T;
}

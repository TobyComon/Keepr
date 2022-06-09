import { AppState } from "../AppState.js"
import { api } from "./AxiosService.js"
import { keepsService } from "./KeepsService.js"


class VaultKeepsService {
    async add(vk) {
        await api.post('api/vaultKeeps', vk)
        keepsService.getVaultKeeps(vk.vaultId)
    }

    async remove(id) {
        await api.delete('api/vaultKeeps/' + id)
        AppState.vaultKeeps = AppState.vaultKeeps.filter(vk => vk.vaultKeepId != id)
    }
}

export const vaultKeepsService = new VaultKeepsService()
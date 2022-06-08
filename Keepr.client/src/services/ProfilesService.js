import { AppState } from "../AppState.js"
import { api } from "./AxiosService.js"
import { keepsService } from "./KeepsService.js"
import { vaultsService } from "./VaultsService.js"


class ProfilesService {
    async getProfile(id) {
        const res = await api.get('api/profiles/' + id)
        AppState.profile = res.data
        await keepsService.getKeepsByProfileId(id)
        await vaultsService.getVaultsByProfileId(id)
    }
}

export const profilesService = new ProfilesService()
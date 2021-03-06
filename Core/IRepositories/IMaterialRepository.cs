namespace SE_training.Core;

public interface IMaterialRepository
{
    Task<(Status status, MaterialDTO material)> CreateMaterialAsync(CreateMaterialDTO material);
    Task<Status> UpdateMaterialAsync(int materialId, CreateMaterialDTO material);
    Task<MaterialDTO> ReadAsync(int MaterialId);
    Task<IReadOnlyCollection<MaterialDTO>> ReadAllAsync();
    Task<Status> DeleteAsync(int MaterialId);
}
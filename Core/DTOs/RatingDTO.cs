namespace SE_training.Core;

public record CreateRatingDTO(int MaterialId, int UserId, int Value);
public record RatingDTO(int Id, int MaterialId, int UserId, int Value) : CreateRatingDTO(MaterialId, UserId, Value);
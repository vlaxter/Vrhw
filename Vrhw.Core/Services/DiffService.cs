using System;
using System.Collections.Generic;
using System.Text;
using Vrhw.Shared.Helpers;
using Vrhw.Shared.Interfaces;
using Vrhw.Shared.Models;

namespace Vrhw.Core.Services
{
    public class DiffService : IDiffService
    {
        private readonly IDiffRepository _diffReository;

        public DiffService(IDiffRepository diffRepository)
        {
            _diffReository = diffRepository;
        }

        public bool Left(int id, string data)
        {
            if (data.IsBase64String())
            {
                _diffReository.UpsertDiff(id, data, null);
                return true;
            }

            return false;
        }

        public bool Right(int id, string data)
        {
            if (data.IsBase64String())
            {
                _diffReository.UpsertDiff(id, null, data);
                return true;
            }

            return false;
        }

        public object GetDiff(int id)
        {
            var diff = _diffReository.GetDiff(id);

            if (diff == null)
            {
                return null;
            }

            var leftData = Convert.FromBase64String(diff.Left);
            var decodedLeftString = Encoding.UTF8.GetString(leftData);
            var rightData = Convert.FromBase64String(diff.Right);
            var decodedRightString = Encoding.UTF8.GetString(rightData);

            if (decodedLeftString == decodedRightString)
            {
                return new
                {
                    diffResultType = DiffMessages.EqualsResponse
                };
            }
            else if (decodedLeftString.Length != decodedRightString.Length)
            {
                return new
                {
                    diffResultType = DiffMessages.SizeDoNotMatchResponse
                };
            }

            var leftPieces = decodedLeftString.ToCharArray();
            var rightPieces = decodedRightString.ToCharArray();

            var differences = new List<DiffSection>();
            DiffSection currentDiff = null;

            for (int i = 0; i < leftPieces.Length; i++)
            {
                if (currentDiff == null)
                {
                    if (leftPieces[i] != rightPieces[i])
                    {
                        currentDiff = new DiffSection
                        {
                            Offset = i,
                            Length = 1
                        };
                    }
                }
                else
                {
                    if (leftPieces[i] != rightPieces[i])
                    {
                        currentDiff.Length++;
                    }
                    else
                    {
                        differences.Add(currentDiff);
                        currentDiff = null;
                    }
                }
            }

            if (currentDiff != null)
            {
                differences.Add(currentDiff);
            }

            return new
            {
                diffResultType = DiffMessages.ContentDoNotMatchResponse,
                diffs = differences
            };
        }
    }
}